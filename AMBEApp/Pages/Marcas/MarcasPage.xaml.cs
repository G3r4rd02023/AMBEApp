using AMBEApp.Models;
using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages.Marcas;

public partial class MarcasPage : ContentPage
{
    private readonly MarcasViewModel _viewModel;
    public MarcasPage()
	{
		InitializeComponent();
        _viewModel = new MarcasViewModel();
        BindingContext = _viewModel;
        CargarMarcas();
        txtFiltro.TextChanged += (sender, e) => FiltrarMarcas();
    }

    private async void CargarMarcas()
    {
        ServicioMarcas servicioMarcas = new();
        var registros = await servicioMarcas.ObtenerLista();
        _viewModel.Marcas = registros;
    }

    private void OnGenerarPdfClicked(object sender, EventArgs e)
    {
    }

    private void OnImprimirClicked(object sender, EventArgs e)
    {
    }
    //eliminar boton
    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        var boton = (Button)sender;
        if (boton.BindingContext is Marca marca)
        {
            bool confirmacion = await DisplayAlert("Confirmar Eliminación", "¿Estás seguro de que deseas eliminar este objeto?", "Sí", "No");
            if (confirmacion)
            {
                ServicioMarcas servicioMarcas = new();
                bool registroEliminado = await servicioMarcas.EliminarMarca(marca.IdMarca);
                if (registroEliminado)
                {
                    await DisplayAlert("Éxito", "marca eliminada correctamente", "OK");
                    ServicioUsuario servicioUsuario = new();
                    var usuario = ServicioUsuario.UsuarioAutenticado;
                    int userId = await servicioUsuario.ObtenerIdUsuario(usuario);

                    await ServicioBitacora.AgregarRegistro(userId, marca.IdInstituto, "Eliminó", "Marcas");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un problema al eliminar la marca. Por favor, intenta nuevamente.", "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener la marca para eliminar", "OK");
        }
    }

    private void OnSearchIconTapped(object sender, EventArgs e)
    {
        DisplayAlert("Búsqueda", "Realizar búsqueda...", "Aceptar");
    }

    private async void OnCrearNuevoRegistroClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new CrearMarcaPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error : {ex.Message}", "OK");
            return;
        }
    }

    //filtrar
    private async void FiltrarMarcas()
    {
        ServicioMarcas servicioMarcas = new();
        var marcas = await servicioMarcas.ObtenerLista();
        var marcasFiltrado = marcas.Where(m =>
        m.NombreMarca.Contains(txtFiltro.Text, StringComparison.OrdinalIgnoreCase));
        _viewModel.Marcas = new List<Marca>(marcasFiltrado);
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {

        var boton = (Button)sender;
        if (boton.BindingContext is Marca marca)
        {
            await Navigation.PushAsync(new EditarMarcaPage(marca));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener la marca para editar", "OK");
        }

    }

}