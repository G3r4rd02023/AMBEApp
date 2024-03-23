using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages.Marcas;

public partial class CrearMarcaPage : ContentPage
{
	public CrearMarcaPage()
	{
		InitializeComponent();
	}

    private async void CrearMarca(object sender, EventArgs e)
    {
        try
        {

            string nombreMarca = TxtNombreMarca.Text;

            var username = ServicioUsuario.UsuarioAutenticado;
            ServicioUsuario servicioUsuario = new();
            var usuarios = await servicioUsuario.ObtenerLista();
            var usuarioEncontrado = usuarios.FirstOrDefault(u => u.Usuario == username);

            int idInstituto = usuarioEncontrado.IdInstituto;

            if (!ServicioValidaciones.ValidarEntradas(nombreMarca))
            {
                await DisplayAlert("Error", "Por favor, llena los campos", "OK");
                return;
            }


            var nuevaMarca = new Marca()
            {
                NombreMarca = nombreMarca,
                IdInstituto = idInstituto,
                Estado = "Activo",
                CreadoPor = username,
                FechaDeCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaDeModificacion = DateTime.Now
            };

            string objetoJson = System.Text.Json.JsonSerializer.Serialize(nuevaMarca);

            ServicioMarcas servicioMarca = new();
            bool marcaExiste = await servicioMarca.MarcaExiste(nombreMarca);

            if (marcaExiste)
            {
                await DisplayAlert("Error", "La marca ya existe.", "OK");
            }
            else
            {
                var httpClient = new HttpClient();
                var content = new StringContent(objetoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Marcas", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Marca creada correctamente", "OK");
                    int idUsuario = await servicioUsuario.ObtenerIdUsuario(username);
                    await ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Marca");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al crear la marca. Por favor, intenta nuevamente.", "OK");
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