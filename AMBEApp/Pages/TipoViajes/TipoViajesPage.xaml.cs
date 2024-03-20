
using AMBEApp.Services;
using AMBEApp.ViewModels;
namespace AMBEApp.Pages.TipoViajes;


public partial class TipoViajesPage : ContentPage
{

    private readonly TipoViajeViewModel _viewModel;
    public TipoViajesPage()
	{
		InitializeComponent();
        _viewModel = new TipoViajeViewModel();
        BindingContext = _viewModel;
        CargarTiposViaje();
    }

    private async void CargarTiposViaje()
    {
        ServicioTipoViaje servicioTipoViaje = new();
        var registros = await servicioTipoViaje.ObtenerLista();
        _viewModel.TipoViajes = registros;
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
            await Navigation.PushAsync(new CrearTipoViajePage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error : {ex.Message}", "OK");
            return;
        }
    }


    private void OnEditarClicked(object sender, EventArgs e)
    {
       
    }
}