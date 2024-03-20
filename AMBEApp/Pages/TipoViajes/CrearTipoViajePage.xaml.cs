using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages.TipoViajes;

public partial class CrearTipoViajePage : ContentPage
{
	public CrearTipoViajePage()
	{
		InitializeComponent();
	}

    private async void CrearTipoViaje(object sender, EventArgs e)
	{
        try
        {                                
            string descripcion = TxtDescripcion.Text;
            string evento = TxtEvento.Text;

            var username = ServicioUsuario.UsuarioAutenticado;

            if (!ServicioValidaciones.ValidarEntradas(evento, descripcion))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var nuevoTipoViaje = new TipoViaje()
            {
                Evento = evento,                
                Descripcion = descripcion,                
                CreadoPor = username,
                FechaDeCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaDeModificacion = DateTime.Now
            };

            string tipoViajeJson = System.Text.Json.JsonSerializer.Serialize(nuevoTipoViaje);

            ServicioTipoViaje servicioTipoViaje = new();
            bool tipoViajeExiste = await servicioTipoViaje.TipoViajeExiste(evento);

            if (tipoViajeExiste)
            {
                await DisplayAlert("Error", "El tipo de viaje ya está existe.", "OK");
            }
            else
            {
                var httpClient = new HttpClient();
                var content = new StringContent(tipoViajeJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/TipoViajes", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "TipoViaje creado correctamente"
                        , "OK");
                    ServicioUsuario servicioUsuario = new();
                    int idUsuario = await servicioUsuario.ObtenerIdUsuario(username);
                    ServicioInstituto servicioInstituto = new();
                    int idInstituto = await servicioInstituto.ObtenerIdInstituto(username);
                    await ServicioBitacora.AgregarRegistro(idUsuario,idInstituto, "Creo", "Tipo Viaje");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al crear el tipo de viaje. Por favor, intenta nuevamente.", "OK");
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