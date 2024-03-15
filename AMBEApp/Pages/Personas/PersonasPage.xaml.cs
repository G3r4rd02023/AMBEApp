using AMBEApp.Models;
using AMBEApp.Services;
using AMBEApp.ViewModels;

namespace AMBEApp.Pages.Personas;

public partial class PersonasPage : ContentPage
{

    private readonly PersonasViewModel _viewModel;
    public PersonasPage()
	{
		InitializeComponent();
        _viewModel = new PersonasViewModel();
        BindingContext = _viewModel;
        CargarPersonas();
    }

    private async void CargarPersonas()
    {              
        ServicioTipoPersona servicioTipoPersona = new();
        var tipoPersonas = await servicioTipoPersona.ObtenerLista();       
        pickerPersona.ItemsSource = tipoPersonas.Select(p => p.TipoPersona).ToList();
    }

    private void OnGenerarPdfClicked(object sender, EventArgs e)
    {
    }

    private void OnImprimirClicked(object sender, EventArgs e)
    {
    }

    private void OnEditarClicked(object sender, EventArgs e)
    {
        
    }

    private void OnEliminarClicked(object sender, EventArgs e)
    {

    }

    private async void FiltrarPersonas(object sender, EventArgs e)
    {
        try
        {
            if (!ServicioValidaciones.ValidarPicker(pickerPersona))
            {
                await DisplayAlert("Error", "Por favor, selecciona una opcion.", "OK");
                return;
            }

            ServicioTipoPersona servicioTipoPersona = new();
            string nombreTipoPersona = pickerPersona.SelectedItem.ToString();
            int idTipoPersona = await servicioTipoPersona.ObtenerIdTipoPersonaPorNombre(pickerPersona.SelectedItem.ToString());
            ServicioPersonas servicioPersonas = new();
            var personas = await servicioPersonas.ObtenerLista();
            var personasFiltradas = personas.Where(p => p.IdTipoPersona == idTipoPersona).ToList();

            _viewModel.Personas = new List<Persona>(personasFiltradas);           
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ha ocurrido un error: {ex.Message}", "OK");
        }
    }
}