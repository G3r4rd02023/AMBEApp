using AMBEApp.Models;
using AMBEApp.Pages.Objetos;
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
        o.NombreObjeto.Contains(txtFiltro.Text, StringComparison.OrdinalIgnoreCase));
        _viewModel.Objetos = new List<Objeto>(objetosFiltrado);
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
   
    private async void OnEditarClicked(object sender, EventArgs e)
    {
        var boton = (Button)sender;
        var objeto = boton.BindingContext as Objeto;
        if (objeto != null)
        {
            // Navegar a la página de edición de usuario y pasar el usuario como parámetro
            await Navigation.PushAsync(new EditarObjetoPage(objeto));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener el objeto para editar", "OK");
        }
    }
}