using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages.Marcas;

public partial class MarcasPage : ContentPage
{
    private readonly MarcasViewModel _viewModel;
    public MarcasPage()
	{
		InitializeComponent();
        _viewModel = new MarcasViewModel();
        BindingContext = _viewModel;
        CargarMarcas();
    }

    private async void CargarMarcas()
    {
        ServicioMarcas servicioMarcas = new();
        var registros = await servicioMarcas.ObtenerLista();
        _viewModel.Marcas = registros;
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
            await Navigation.PushAsync(new CrearMarcaPage());
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