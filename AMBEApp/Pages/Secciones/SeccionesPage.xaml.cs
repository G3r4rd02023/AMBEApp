using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages.Secciones;

public partial class SeccionesPage : ContentPage
{

    private readonly SeccionesViewModel _viewModel;
    public SeccionesPage()
	{
		InitializeComponent();
        _viewModel = new SeccionesViewModel();
        BindingContext = _viewModel;
        CargarSecciones();
    }

    private async void CargarSecciones()
    {
        ServicioSeccion servicioSeccion = new();
        var registros = await servicioSeccion.ObtenerLista();
        _viewModel.Secciones = registros;
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
            await Navigation.PushAsync(new CrearSeccionPage());
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