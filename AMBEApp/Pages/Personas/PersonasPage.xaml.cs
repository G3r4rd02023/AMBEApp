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
        pickerPersona.SelectedIndexChanged += (sender, e) => FiltrarPersonas();
       
    }

    private async void CargarPersonas()
    {
        try
        {           
            ServicioTipoPersona servicioTipoPersona = new();
            var tipoPersonas = await servicioTipoPersona.ObtenerLista();
            pickerPersona.ItemsSource = tipoPersonas.Select(p => p.TipoPersona).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ha ocurrido un error al cargar los tipos de personas: {ex.Message}", "OK");
        }
    }

    private async void FiltrarPersonas()
    {
        try
        {
            if (pickerPersona.SelectedItem != null)
            {
                ServicioTipoPersona servicioTipoPersona = new();
                string nombreTipoPersona = pickerPersona.SelectedItem.ToString();
                int idTipoPersona = await servicioTipoPersona.ObtenerIdTipoPersonaPorNombre(nombreTipoPersona);

                if (idTipoPersona != -1)
                {
                    ServicioPersonas servicioPersonas = new();
                    var personas = await servicioPersonas.ObtenerLista();
                    var personasFiltradas = personas.Where(p => p.IdTipoPersona == idTipoPersona).ToList();

                    _viewModel.Personas = new List<Persona>(personasFiltradas);
                }
                else
                {
                    // Manejar el caso donde no se encuentra el tipo de persona correspondiente al nombre seleccionado
                    await DisplayAlert("Error", "El tipo de persona seleccionado no existe.", "OK");
                }
            }            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ha ocurrido un error al filtrar las personas: {ex.Message}", "OK");
        }
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

    
}