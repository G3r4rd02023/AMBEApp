using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages.Parentescos;

public partial class ParentescosPage : ContentPage
{
    private readonly ParentescosViewModel _viewModel;

    public ParentescosPage()
	{
		InitializeComponent();
        _viewModel = new ParentescosViewModel();
        BindingContext = _viewModel;
        CargarParentescos();
       
    }

    private async void CargarParentescos()
    {
        ServicioParentesco servicioParentesco = new();
        var registros = await servicioParentesco.ObtenerLista();
        _viewModel.Parentescos = registros;
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

    private void OnCrearNuevoRegistroClicked(object sender, EventArgs e)
    {
        DisplayAlert("Nuevo Registro", "Implementa la lógica para crear un nuevo registro.", "Aceptar");
    }

    private void OnEditarClicked(object sender, EventArgs e)
    {
    }
}