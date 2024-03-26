using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages.Unidades;

public partial class CrearUnidadPage : ContentPage
{
	public CrearUnidadPage()
	{
		InitializeComponent();
        CargarConductores();
        CargarMarcas();
        CargarModelos();
	}

    private async void CargarConductores()
    {
        try
        {
            ServicioPersonas servicioPersona = new();
            var personas = await servicioPersona.ObtenerLista();
            var conductores = personas.Where(p => p.IdTipoPersona == 2).ToList();
            pickerConductor.ItemsSource = conductores.Select(r => r.NombreCompleto).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar los conductores: " + ex.Message, "OK");
        }
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

    private async void CargarModelos()
    {
        try
        {
            ServicioModelo servicioModelos = new();
            List<Modelo> lista = await servicioModelos.ObtenerLista();

            pickerModelo.ItemsSource = lista.Select(r => r.NombreModelo).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar las marcas: " + ex.Message, "OK");
        }
    }




    private async void CrearUnidad(object sender, EventArgs e)
    {
        try
        {
            if (!ServicioValidaciones.ValidarPicker(pickerConductor))
            {
                await DisplayAlert("Error", "Por favor, selecciona un conductor.", "OK");
                return;
            }

            if (!ServicioValidaciones.ValidarPicker(pickerMarca))
            {
                await DisplayAlert("Error", "Por favor, selecciona una marca.", "OK");
                return;
            }

            if (!ServicioValidaciones.ValidarPicker(pickerModelo))
            {
                await DisplayAlert("Error", "Por favor, selecciona un modelo.", "OK");
                return;
            }

            var username = ServicioUsuario.UsuarioAutenticado;
            ServicioUsuario servicioUsuario = new();
            var usuarios = await servicioUsuario.ObtenerLista();
            var usuarioEncontrado = usuarios.FirstOrDefault(u => u.Usuario == username);

            int idInstituto = usuarioEncontrado.IdInstituto;


            string numeroUnidad = TxtNumeroUnidad.Text;
            string placa = TxtPlaca.Text;
            string color = TxtColor.Text;
            int capacidad = int.Parse(TxtCapacidad.Text);
            string chasis = TxtChasis.Text;

            ServicioPersonas servicioPersona = new();
            int idConductor = await servicioPersona.ObtenerIdPersonaPorNombre(pickerConductor.SelectedItem.ToString());
            ServicioMarcas servicioMarcas = new();
            int idMarca = await servicioMarcas.ObtenerIdMarcaPorNombre(pickerMarca.SelectedItem.ToString());
            ServicioModelo servicioModelo = new();
            int idModelo = await servicioModelo.ObtenerIdModeloPorNombre(pickerModelo.SelectedItem.ToString());

            if (!ServicioValidaciones.ValidarEntradas(numeroUnidad, placa, color, TxtCapacidad.Text))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var nuevaUnidad = new Unidad()
            {
                NumeroUnidad = numeroUnidad,
                Placa = placa,
                Color = color,
                Capacidad = capacidad,
                Chasis = chasis,
                IdPersonaConductor = idConductor,
                IdMarca = idMarca,
                IdModelo = idModelo,
                IdInstituto = idInstituto,
                CreadoPor = username,
                FechaDeCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaDeModificacion = DateTime.Now
            };

            string objetoJson = System.Text.Json.JsonSerializer.Serialize(nuevaUnidad);

            ServicioUnidades servicioUnidad = new();
            bool unidadExiste = await servicioUnidad.UnidadExiste(placa);

            if (unidadExiste)
            {
                await DisplayAlert("Error", "La unidad ya existe.", "OK");
            }
            else
            {
                var httpClient = new HttpClient();
                var content = new StringContent(objetoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Unidades", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Marca creada correctamente", "OK");
                    int idUsuario = await servicioUsuario.ObtenerIdUsuario(username);
                    await ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Unidad");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al crear la unidad. Por favor, intenta nuevamente.", "OK");
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