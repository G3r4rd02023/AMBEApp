using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages.Grados;

public partial class CrearGradoPage : ContentPage
{
    public CrearGradoPage()
    {
        InitializeComponent();
    }

    private async void CrearGrado(object sender, EventArgs e)
    {
        try
        {

            var username = ServicioUsuario.UsuarioAutenticado;
            ServicioUsuario servicioUsuario = new();
            var usuarios = await servicioUsuario.ObtenerLista();
            var usuarioEncontrado = usuarios.FirstOrDefault(u => u.Usuario == username);

            int idInstituto = usuarioEncontrado.IdInstituto;
            string nombreGrado = TxtNombreGrado.Text;




            if (!ServicioValidaciones.ValidarEntradas(nombreGrado))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var nuevoGrado = new Grado()
            {
                IdInstituto = idInstituto,
                NombreGrado = nombreGrado,
                Estado = "Activo",
                CreadoPor = username,
                FechaDeCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaDeModificacion = DateTime.Now
            };

            string gradoJson = System.Text.Json.JsonSerializer.Serialize(nuevoGrado);

            ServicioGrados servicioGrado = new();
            bool gradoExiste = await servicioGrado.GradoExiste(nombreGrado);

            if (gradoExiste)
            {
                await DisplayAlert("Error", "El grado ya está existe.", "OK");
            }
            else
            {
                var httpClient = new HttpClient();
                var content = new StringContent(gradoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Grados", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Grado creado correctamente"
                        , "OK");

                    int idUsuario = await servicioUsuario.ObtenerIdUsuario(username!);
                    await ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Grados");
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