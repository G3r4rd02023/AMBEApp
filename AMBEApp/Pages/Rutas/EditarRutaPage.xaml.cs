using AMBEApp.Models;
using AMBEApp.Services;

namespace AMBEApp.Pages.Rutas;

public partial class EditarRutaPage : ContentPage
{
    public Ruta Ruta { get; set; }
    public EditarRutaPage(Ruta ruta)
    {
        InitializeComponent();
        Ruta = ruta;
        TxtRuta.Text = ruta.NombreRuta ?? string.Empty;
        TxtOrigen.Text = ruta.Origen ?? string.Empty;
        TxtDestino.Text = ruta.Destino ?? string.Empty;
        TxtDepartamento.Text = ruta.Departamento ?? string.Empty;
        TxtDistancia.Text = ruta.Distancia.ToString();
        TxtMunicipio.Text = ruta.Municipio ?? string.Empty;
        TxtColonias.Text = ruta.Colonias ?? string.Empty;
        TxtDescripcion.Text = ruta.Descripcion ?? string.Empty;
    }

    private async void Editar(object sender, EventArgs e)
    {
        var rutaActual = Ruta;
        int idInstituto = rutaActual.IdInstituto;
        string nombreRuta = TxtRuta.Text;
        string origen = TxtOrigen.Text;
        string destino = TxtDestino.Text;
        string departamento = TxtDepartamento.Text;
        string municipio = TxtMunicipio.Text;
        string colonias = TxtColonias.Text;
        string descripcion = TxtDescripcion.Text;

        var usuario = ServicioUsuario.UsuarioAutenticado;

        if (!ServicioValidaciones.ValidarEntradas(nombreRuta, origen, destino, TxtDistancia.Text, colonias, departamento, descripcion, municipio))
        {
            await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
            return;
        }

        var rutaEditada = new Ruta()
        {
            IdRuta = rutaActual.IdRuta,
            IdInstituto = idInstituto,
            NombreRuta = nombreRuta,
            Origen = origen,
            Distancia = rutaActual.Distancia,
            Colonias = colonias,
            Departamento = departamento,
            Municipio = municipio,
            Descripcion = descripcion,
            CreadoPor = usuario,
            FechaDeCreacion = rutaActual.FechaDeCreacion,
            ModificadoPor = usuario,
            FechaDeModificacion = DateTime.Now
        };

        string rutaJson = System.Text.Json.JsonSerializer.Serialize(rutaEditada);
        ServicioRutas servicioRutas = new();
        bool registroExitoso = await servicioRutas.ActualizarRuta(rutaJson, rutaEditada);
        if (registroExitoso)
        {
            await DisplayAlert("Éxito", "Ruta actualizada correctamente", "OK");

            ServicioUsuario servicioUsuario = new();
            int userId = await servicioUsuario.ObtenerIdUsuario(usuario);

            await ServicioBitacora.AgregarRegistro(userId, idInstituto, "Editó", "Rutas");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al actualizar la ruta. Por favor, intenta nuevamente.", "OK");
        }
    }
}