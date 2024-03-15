using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages;

public partial class RolesPage : ContentPage
{
    private readonly RolesViewModel _viewModel;
    public RolesPage()
    {
        InitializeComponent();
        _viewModel = new RolesViewModel();
        BindingContext = _viewModel;
        CargarRoles();
    }

    private async void CargarRoles()
    {
        ServicioRoles roles = new();
        var registros = await roles.ObtenerLista();
        _viewModel.Roles = registros;
    }

    private async void CrearRol_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new CrearRolPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error : {ex.Message}", "OK");
            return;
        }
    }

    private void OnEditarClicked(object sender, EventArgs e)
    {
        DisplayAlert("Editar", "Editar Rol", "Aceptar");
    }

    private void OnEliminarClicked(object sender, EventArgs e)
    {
        
    }

    private void OnSearchIconTapped(object sender, EventArgs e)
    {
        DisplayAlert("Búsqueda", "Realizar búsqueda...", "Aceptar");
    }

    private void OnGenerarPdfClicked(object sender, EventArgs e)
    {
    }

    private void OnImprimirClicked(object sender, EventArgs e)
    {
    }
}