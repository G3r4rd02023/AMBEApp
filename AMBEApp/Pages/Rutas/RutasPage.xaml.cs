using AMBEApp.Models;
using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages.Rutas;

public partial class RutasPage : ContentPage
{
    private readonly RutasViewModel _viewModel;
    public RutasPage()
	{
		InitializeComponent();
        _viewModel = new RutasViewModel();
        BindingContext = _viewModel;
        CargarRutas();
        txtFiltro.TextChanged += (sender, e) => FiltrarRutas();
    }

    private async void CargarRutas()
    {
        ServicioRutas servicioRutas = new();
        var registros = await servicioRutas.ObtenerLista();
        _viewModel.Rutas = registros;
    }

    private async void FiltrarRutas()
    {
        ServicioRutas servicioRutas = new();
        var rutas = await servicioRutas.ObtenerLista();
        var rutasFiltrado = rutas.Where(o =>
        o.NombreRuta.Contains(txtFiltro.Text, StringComparison.OrdinalIgnoreCase));
        _viewModel.Rutas = new List<Ruta>(rutasFiltrado);
    }

    private void OnGenerarPdfClicked(object sender, EventArgs e)
    {
    }

    private void OnImprimirClicked(object sender, EventArgs e)
    {
    }
    private void OnEliminarClicked(object sender, EventArgs e)
    {

    }

    private void OnSearchIconTapped(object sender, EventArgs e)
    {
        DisplayAlert("Búsqueda", "Realizar búsqueda...", "Aceptar");
    }

    private async void OnCrearNuevoRegistroClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new CrearRutaPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error : {ex.Message}", "OK");
            return;
        }
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        var boton = (Button)sender;
        if (boton.BindingContext is Ruta ruta)
        {
            await Navigation.PushAsync(new EditarRutaPage(ruta));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener el ruta para editar", "OK");
        }
    }
}