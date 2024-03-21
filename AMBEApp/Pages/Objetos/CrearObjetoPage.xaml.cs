using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages;

public partial class CrearObjetoPage : ContentPage
{
	public CrearObjetoPage()
	{
		InitializeComponent();
        CargarInstitutos();
	}

    private async void CargarInstitutos()
    {
        try
        {
            ServicioInstituto servicioInstituto = new();
            List<Institutos> lista = await servicioInstituto.ObtenerLista();

            pickerInstituto.ItemsSource = lista.Select(r => r.NombreInstituto).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar los institutos: " + ex.Message, "OK");
        }
    }
    private async void CrearObjeto(object sender, EventArgs e)
    {
        try
        {

            if (!ServicioValidaciones.ValidarPicker(pickerInstituto))
            {
                await DisplayAlert("Error", "Por favor, selecciona un instituto.", "OK");
                return;
            }

            ServicioInstituto servicioInstituto = new();
            int idInstituto = await servicioInstituto.ObtenerIdInstitutoPorNombre(pickerInstituto.SelectedItem.ToString());
            string objeto = TxtObjeto.Text;
            string descripcion = TxtDescripcion.Text;
            string tipoObjeto = TxtTipoObjeto.Text;
            
            var username = ServicioUsuario.UsuarioAutenticado;

            if (!ServicioValidaciones.ValidarEntradas(objeto, descripcion, tipoObjeto))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var nuevoObjeto = new Objeto()
            {
                IdInstituto = idInstituto,
                NombreObjeto = objeto,
                Descripcion = descripcion,
                TipoObjeto = tipoObjeto,
                CreadoPor = username,
                FechaDeCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaDeModificacion = DateTime.Now
            };

            string objetoJson = System.Text.Json.JsonSerializer.Serialize(nuevoObjeto);

            ServicioObjeto servicioObjeto = new();
            bool objetoExiste = await servicioObjeto.ObjetoExiste(objeto);

            if (objetoExiste)
            {
                await DisplayAlert("Error", "El objeto ya está existe.", "OK");
            }
            else
            {
                var httpClient = new HttpClient();
                var content = new StringContent(objetoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Objetos", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Objeto creado correctamente"
                        , "OK");
                    //ServicioUsuario servicioUsuario = new();
                    //int idUsuario = await servicioUsuario.ObtenerIdUsuario(username!);
                    //ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Roles");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al crear el objeto. Por favor, intenta nuevamente.", "OK");
                }

            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Por favor complete todos los campos : {ex.Message}", "OK");
            return;
        }

    }
}