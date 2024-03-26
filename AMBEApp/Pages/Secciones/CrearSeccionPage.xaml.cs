using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages.Secciones;

public partial class CrearSeccionPage : ContentPage
{
	public CrearSeccionPage()
	{
		InitializeComponent();
        CargarGrados();
	}

    private async void CargarGrados()
    {
        try
        {
            ServicioGrados servicioGrados = new();
            List<Grado> lista = await servicioGrados.ObtenerLista();

            pickerGrados.ItemsSource = lista.Select(r => r.NombreGrado).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar los grados: " + ex.Message, "OK");
        }
    }

    private async void CrearSeccion(object sender, EventArgs e)
    {
        try
        {
            if (!ServicioValidaciones.ValidarPicker(pickerGrados))
            {
                await DisplayAlert("Error", "Por favor, selecciona un grado.", "OK");
                return;
            }

            ServicioGrados servicioGrados = new();
            string nombreSeccion = TxtNombreSeccion.Text;
            int idGrado = await servicioGrados.ObtenerIdGradoPorNombre(pickerGrados.SelectedItem.ToString());

            var username = ServicioUsuario.UsuarioAutenticado;
            ServicioUsuario servicioUsuario = new();
            var usuarios = await servicioUsuario.ObtenerLista();
            var usuarioEncontrado = usuarios.FirstOrDefault(u => u.Usuario == username);

            int idInstituto = usuarioEncontrado.IdInstituto;

            if (!ServicioValidaciones.ValidarEntradas(nombreSeccion))
            {
                await DisplayAlert("Error", "Por favor, llena los campos", "OK");
                return;
            }


            var nuevaSeccion = new Seccion()
            {
                NombreSeccion = nombreSeccion,
                IdGrado = idGrado,
                IdInstituto = idInstituto,
                Estado = "Activo",
                CreadoPor = username,
                FechaDeCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaDeModificacion = DateTime.Now
            };

            string objetoJson = System.Text.Json.JsonSerializer.Serialize(nuevaSeccion);

            ServicioSeccion servicioSeccion = new();
            bool seccionExiste = await servicioSeccion.SeccionExiste(nombreSeccion);

            if (seccionExiste)
            {
                await DisplayAlert("Error", "La seccion ya existe.", "OK");
            }
            else
            {
                var httpClient = new HttpClient();
                var content = new StringContent(objetoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Secciones", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Seccion creada correctamente", "OK");
                    int idUsuario = await servicioUsuario.ObtenerIdUsuario(username);
                    await ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Seccion");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al crear la seccion. Por favor, intenta nuevamente.", "OK");
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