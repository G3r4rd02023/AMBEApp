using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages.Modelos;

public partial class ModelosPage : ContentPage
{
    private readonly ModelosViewModel _viewModel;
    public ModelosPage()
	{
		InitializeComponent();
        _viewModel = new ModelosViewModel();
        BindingContext = _viewModel;
        CargarModelos();
    }

    private async void CargarModelos()
    {
        ServicioModelo servicioModelos = new();
        var registros = await servicioModelos.ObtenerLista();
        _viewModel.Modelos = registros;
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
            await Navigation.PushAsync(new CrearModeloPage());
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