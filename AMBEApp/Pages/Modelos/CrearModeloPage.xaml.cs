using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages.Modelos;

public partial class CrearModeloPage : ContentPage
{
    public CrearModeloPage()
    {
        InitializeComponent();
        CargarMarcas();
    }

    private async void CargarMarcas()
    {
        try
        {
            ServicioMarcas servicioMarcas = new();
            List<Marca> lista = await servicioMarcas.ObtenerLista();

            pickerMarca.ItemsSource = lista.Select(r => r.NombreMarca).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar las marcas: " + ex.Message, "OK");
        }
    }

    private async void CrearModelo(object sender, EventArgs e)
    {
        try
        {

            if (!ServicioValidaciones.ValidarPicker(pickerMarca))
            {
                await DisplayAlert("Error", "Por favor, selecciona un instituto.", "OK");
                return;
            }

            ServicioMarcas servicioMarcas = new();
            string nombreModelo = TxtNombreModelo.Text;
            int idMarca = await servicioMarcas.ObtenerIdMarcaPorNombre(pickerMarca.SelectedItem.ToString());

            var username = ServicioUsuario.UsuarioAutenticado;

            if (!ServicioValidaciones.ValidarEntradas(nombreModelo))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var nuevoModelo = new Modelo()
            {
                IdMarca = idMarca,
                NombreModelo = nombreModelo,
                Estado = "Activo",
                CreadoPor = username,
                FechaDeCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaDeModificacion = DateTime.Now
            };

            string modeloJson = System.Text.Json.JsonSerializer.Serialize(nuevoModelo);

            ServicioModelo servicioModelo = new();
            bool modeloExiste = await servicioModelo.ModeloExiste(nombreModelo);

            if (modeloExiste)
            {
                await DisplayAlert("Error", "El modelo ya está existe.", "OK");
            }
            else
            {
                var httpClient = new HttpClient();
                var content = new StringContent(modeloJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Modelos", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Modelo creado correctamente", "OK");
                    ServicioUsuario servicioUsuario = new();
                    int idUsuario = await servicioUsuario.ObtenerIdUsuario(username);
                    ServicioInstituto servicioInstituto = new();
                    int idInstituto = await servicioInstituto.ObtenerIdInstituto(username);
                    await ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Modelo");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al crear el modelo. Por favor, intenta nuevamente.", "OK");
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