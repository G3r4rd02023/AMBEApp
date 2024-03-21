using AMBEApp.Models;
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
        txtFiltro.TextChanged += (sender, e) => FiltrarObjetos();
    }

    private async void CargarObjetos()
    {
        ServicioObjeto servicioObjeto = new();
        var registros = await servicioObjeto.ObtenerLista();
        _viewModel.Objetos = registros;
    }


    private async void OnCrearNuevoRegistroClicked(object sender, EventArgs e)
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

    private async void FiltrarObjetos()
    {
        ServicioObjeto servicioObjeto = new();
        var objetos = await servicioObjeto.ObtenerLista();
        var objetosFiltrado = objetos.Where(o =>
        o.Objeto.Contains(txtFiltro.Text, StringComparison.OrdinalIgnoreCase));
        _viewModel.Objetos = new List<Objetos>(objetosFiltrado);
    }

    private void OnGenerarPdfClicked(object sender, EventArgs e)
    {
    }

    //private void OnImprimirClicked(object sender, EventArgs e)
    //{
    //}

    private void OnEliminarClicked(object sender, EventArgs e)
    {

    }

    private void OnSearchIconTapped(object sender, EventArgs e)
    {
          
    }
   
    private void OnEditarClicked(object sender, EventArgs e)
    {

    }
}