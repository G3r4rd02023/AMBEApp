using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages;

public partial class CrearObjetoPage : ContentPage
{
	public CrearObjetoPage()
	{
		InitializeComponent();
        
	}

    
    private async void CrearObjeto(object sender, EventArgs e)
    {
        try
        {

            var username = ServicioUsuario.UsuarioAutenticado;
            ServicioUsuario servicioUsuario = new();
            var usuarios = await servicioUsuario.ObtenerLista();
            var usuarioEncontrado = usuarios.FirstOrDefault(u => u.Usuario == username);

            int idInstituto = usuarioEncontrado.IdInstituto;
            string objeto = TxtObjeto.Text;
            string descripcion = TxtDescripcion.Text;
            string tipoObjeto = TxtTipoObjeto.Text;
            
           

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
                    
                    int idUsuario = await servicioUsuario.ObtenerIdUsuario(username!);
                    await ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Roles");
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