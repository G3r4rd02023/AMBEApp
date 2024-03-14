using AMBEApp.Models;
using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages;

public partial class UsuariosPage : ContentPage
{

    private readonly UsuariosViewModel _viewModel;
    public UsuariosPage()
	{
		InitializeComponent();
        _viewModel = new UsuariosViewModel();
        BindingContext = _viewModel;
        CargarUsuarios();
    }

    private async void CargarUsuarios()
    {
        ServicioUsuario servicioUsuario = new();
        var usuarios = await servicioUsuario.ObtenerLista();
        _viewModel.Usuarios = usuarios;
    }

    private void OnRegresarClicked(object sender, EventArgs e)
    {
    }

    private void OnGenerarPdfClicked(object sender, EventArgs e)
    {
    }

    private void OnImprimirClicked(object sender, EventArgs e)
    {
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        var boton = (Button)sender;
        var usuario = boton.BindingContext as Usuarios;
        if (usuario != null)
        {
            // Navegar a la página de edición de usuario y pasar el usuario como parámetro
            await Navigation.PushAsync(new EditarUsuarioPage(usuario));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener el usuario para editar", "OK");
        }
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
}