using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages;

public partial class InstitutosPage : ContentPage
{

    private readonly InstitutoViewModel _viewModel;
    public InstitutosPage()
	{
		InitializeComponent();
        _viewModel = new InstitutoViewModel();
        BindingContext = _viewModel; 
        CargarInstitutos();
    }

    private async void CargarInstitutos()
    {
        ServicioInstituto servicioInstituto = new();
        var registros = await servicioInstituto.ObtenerLista();
        _viewModel.Institutos = registros;
    }

    private async void CrearInstituto_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new CrearInstitutoPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error : {ex.Message}", "OK");
            return;
        }
    }

}
