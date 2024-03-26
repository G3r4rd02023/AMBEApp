using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages.Rutas;

public partial class CrearRutaPage : ContentPage
{
	public CrearRutaPage()
	{
		InitializeComponent();
	}

    private async void CrearRuta(object sender, EventArgs e)
	{
		try
		{
            var username = ServicioUsuario.UsuarioAutenticado;
            ServicioUsuario servicioUsuario = new();
            var usuarios = await servicioUsuario.ObtenerLista();
            var usuarioEncontrado = usuarios.FirstOrDefault(u => u.Usuario == username);

            int idInstituto = usuarioEncontrado.IdInstituto;
			string nombreRuta = TxtRuta.Text;
			string origen = TxtOrigen.Text;
			string destino = TxtDestino.Text;
			decimal distancia = Convert.ToDecimal(TxtDistancia.Text);
			string colonias = TxtColonias.Text;
			string departamento = TxtDepartamento.Text;
			string municipio = TxtMunicipio.Text;
			string descripcion = TxtDescripcion.Text;

            if (!ServicioValidaciones.ValidarEntradas(nombreRuta,origen,destino,TxtDistancia.Text,colonias,departamento,descripcion,municipio))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var nuevaRuta = new Ruta()
            {
                IdInstituto = idInstituto,
                NombreRuta = nombreRuta,
                Origen = origen,
                Destino = destino,
                Distancia = distancia,
                Colonias = colonias,
                Departamento = departamento,
                Municipio = municipio,
                Descripcion = descripcion,                
                CreadoPor = username,
                FechaDeCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaDeModificacion = DateTime.Now
            };

            string rutaJson = System.Text.Json.JsonSerializer.Serialize(nuevaRuta);

            ServicioRutas servicioRuta = new();
            bool rutaExiste = await servicioRuta.RutaExiste(nombreRuta);
            
            if (rutaExiste)
            {
                await DisplayAlert("Error", "La ruta ya está existe.", "OK");
            }
            else
            {
                var httpClient = new HttpClient();
                var content = new StringContent(rutaJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Rutas", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Objeto creado correctamente"
                        , "OK");

                    int idUsuario = await servicioUsuario.ObtenerIdUsuario(username!);
                    await ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Rutas");
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