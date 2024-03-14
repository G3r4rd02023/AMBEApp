using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages;

public partial class ObjetosPage : ContentPage
{
    private readonly ObjetosViewModel _viewModel;
    public ObjetosPage()
	{
		InitializeComponent();
        _viewModel = new ObjetosViewModel();
        BindingContext = _viewModel;
        CargarObjetos();
    }

    private async void CargarObjetos()
    {
        ServicioObjeto servicioObjeto = new();
        var registros = await servicioObjeto.ObtenerLista();
        _viewModel.Objetos = registros;
    }


    private async void CrearObjeto_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new CrearObjetoPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error : {ex.Message}", "OK");
            return;
        }
    }
}