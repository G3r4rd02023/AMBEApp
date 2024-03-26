using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages.Unidades;

public partial class UnidadesPage : ContentPage
{
    private readonly UnidadesViewModel _viewModel;
    public UnidadesPage()
    {
        InitializeComponent();
        _viewModel = new UnidadesViewModel();
        BindingContext = _viewModel;
        CargarUnidades();
    }

    private async void CargarUnidades()
    {
        ServicioUnidades servicioUnidades = new();
        var registros = await servicioUnidades.ObtenerLista();
        _viewModel.Unidades = registros;
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
            await Navigation.PushAsync(new CrearUnidadPage());
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