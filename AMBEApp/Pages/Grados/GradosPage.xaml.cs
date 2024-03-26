using AMBEApp.ViewModels;
using AMBEApp.Services;
using AMBEApp.Pages.Grados;
using AMBEApp.Models;


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
        txtFiltro.TextChanged += (sender, e) => FiltrarGrados();
    }


    private async void CargarGrados()
    {       
        ServicioGrados servicioGrados = new();
        var registros = await servicioGrados.ObtenerLista();        
        _viewModel.Grados = registros;
    }


    private async void FiltrarGrados()
    {
        ServicioGrados servicioGrados = new();
        var grados = await servicioGrados.ObtenerLista();
        var gradosFiltrados = grados.Where(o =>
        o.NombreGrado.Contains(txtFiltro.Text, StringComparison.OrdinalIgnoreCase));
        _viewModel.Grados = new List<Grado>(gradosFiltrados);
    }

    private void OnGenerarPdfClicked(object sender, EventArgs e)
    {
    }

    private void OnImprimirClicked(object sender, EventArgs e)
    {
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        var boton = (Button)sender;
        if (boton.BindingContext is Grado grado)
        {
            bool confirmacion = await DisplayAlert("Confirmar Eliminación", "¿Estás seguro de que deseas eliminar este grado?", "Sí", "No");
            if (confirmacion)
            {
                ServicioGrados servicioGrados = new();
                bool registroEliminado = await servicioGrados.EliminarGrado(grado.IdGrado);
                if (registroEliminado)
                {
                    await DisplayAlert("Éxito", "grado eliminado correctamente", "OK");
                    ServicioUsuario servicioUsuario = new();
                    var usuario = ServicioUsuario.UsuarioAutenticado;
                    int userId = await servicioUsuario.ObtenerIdUsuario(usuario);

                    await ServicioBitacora.AgregarRegistro(userId, grado.IdInstituto, "Eliminó", "Grados");
                    FiltrarGrados();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrió un problema al eliminar el grado. Por favor, intenta nuevamente.", "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener el grado para eliminar", "OK");
             
            }

       }

    private void OnSearchIconTapped(object sender, EventArgs e)
    {
        
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

    private async void EditarGrado(object sender, EventArgs e)
    {
        var boton = (Button)sender;
        if (boton.BindingContext is Grado grado)
        {
            await Navigation.PushAsync(new EditarGradoPage(grado));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener el grado para editar", "OK");
        }
    }
}

  