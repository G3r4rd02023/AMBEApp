using AMBEApp.Models;
using AMBEApp.Services;

namespace AMBEApp.Pages.Marcas;

public partial class EditarMarcaPage : ContentPage
{
    public Marca Marca { get; set; }
    public EditarMarcaPage(Marca marca)
    {
        InitializeComponent();
        Marca = marca;

        TxtNombreMarca.Text = marca.NombreMarca ?? string.Empty;
      
    }

    private async void Editar(object sender, EventArgs e)
    {
        var marcaActual = Marca;
        int idInstituto = marcaActual.IdInstituto;
        string nombreMarca = TxtNombreMarca.Text;
     

        var usuario = ServicioUsuario.UsuarioAutenticado;

        if (!ServicioValidaciones.ValidarEntradas(nombreMarca))
        {
            await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
            return;
        }

        var marcaEditada = new Marca()
        {
            IdMarca = marcaActual.IdMarca,
            IdInstituto = idInstituto,
            NombreMarca = nombreMarca,
            Estado = "Activo",
            CreadoPor = usuario,
            FechaDeCreacion = marcaActual.FechaDeCreacion,
            ModificadoPor = usuario,
            FechaDeModificacion = DateTime.Now
        };

        string marcaJson = System.Text.Json.JsonSerializer.Serialize(marcaEditada);
        ServicioMarcas servicioMarca = new();
        bool registroExitoso = await servicioMarca.ActualizarMarca(marcaJson, marcaEditada);
        if (registroExitoso)
        {
            await DisplayAlert("Éxito", "Marca actualizada correctamente", "OK");

            ServicioUsuario servicioUsuario = new();
            int userId = await servicioUsuario.ObtenerIdUsuario(usuario);

            await ServicioBitacora.AgregarRegistro(userId, 1, "Editó", "Marca");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al actualizar la marca. Por favor, intenta nuevamente.", "OK");
        }
    }
}

