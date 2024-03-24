using AMBEApp.Models;
using AMBEApp.Services;


namespace AMBEApp.Pages.Grados;

public partial class EditarGradoPage : ContentPage
{
    public Grado Grado { get; set; }
    public EditarGradoPage(Grado grado)
    {
        InitializeComponent();
        Grado = grado;

        TxtNombreGrado.Text = grado.NombreGrado ?? string.Empty;
    }
    private async void Editar(object sender, EventArgs e)
        {
            var gradoActual = Grado;
            int idInstituto = gradoActual.IdInstituto;
            string nombreGrado = TxtNombreGrado.Text;
            

            var usuario = ServicioUsuario.UsuarioAutenticado;

        if (!ServicioValidaciones.ValidarEntradas(nombreGrado))
        {
            await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
            return;
        }

        var gradoEditado = new Grado()
        {
            IdGrado = gradoActual.IdGrado,
            IdInstituto = idInstituto,
            NombreGrado = nombreGrado,
            CreadoPor = usuario,
            FechaDeCreacion = DateTime.Now,
            ModificadoPor = usuario,
            FechaDeModificacion = DateTime.Now
        };

        string gradoJson = System.Text.Json.JsonSerializer.Serialize(gradoEditado);
        ServicioGrados servicioGrados = new();
        bool registroExitoso = await servicioGrados.ActualizarGrado(gradoJson, gradoEditado);
        if (registroExitoso)
        {
            await DisplayAlert("Éxito", "Grado actualizado correctamente", "OK");

            ServicioUsuario servicioUsuario = new();
            int userId = await servicioUsuario.ObtenerIdUsuario(usuario);


            await ServicioBitacora.AgregarRegistro(userId, gradoActual.IdInstituto, "Editó", "Grados");


            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al actualizar el grado. Por favor, intenta nuevamente.", "OK");
        }
    }

}