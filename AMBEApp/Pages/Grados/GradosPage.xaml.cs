using AMBEApp.ViewModels;
using AMBEApp.Services;

namespace AMBEApp.Pages.Grados;

public partial class GradosPage : ContentPage
{
    private readonly GradosViewModel _viewModel;
    public GradosPage()
	{
		InitializeComponent();
        _viewModel = new GradosViewModel();
        BindingContext = _viewModel;
        CargarGrados();
    }


    private async void CargarGrados()
    {
        ServicioGrados servicioGrados = new();
        var registros = await servicioGrados.ObtenerLista();
        _viewModel.Grados = registros;
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
            await Navigation.PushAsync(new CrearGradoPage());
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