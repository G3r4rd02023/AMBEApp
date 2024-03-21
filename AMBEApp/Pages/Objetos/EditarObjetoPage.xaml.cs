using AMBEApp.Models;
using AMBEApp.Services;

namespace AMBEApp.Pages.Objetos;

public partial class EditarObjetoPage : ContentPage
{
    public Objeto Objeto { get; set; }
    public EditarObjetoPage(Objeto objeto)
	{
		InitializeComponent();
		Objeto = objeto;
       
        TxtObjeto.Text = objeto.NombreObjeto ?? string.Empty;
		TxtDescripcion.Text = objeto.Descripcion ?? string.Empty;
		TxtTipoObjeto.Text = objeto.TipoObjeto ?? string.Empty;
	}

    private async void Editar(object sender, EventArgs e)
	{
		var objetoActual = Objeto;
		int idInstituto = objetoActual.IdInstituto;
		string nombreObjeto = TxtObjeto.Text;
		string descripcion = TxtDescripcion.Text;
		string tipoObjeto = TxtTipoObjeto.Text;

        var usuario = ServicioUsuario.UsuarioAutenticado;

        if (!ServicioValidaciones.ValidarEntradas(nombreObjeto, descripcion, tipoObjeto))
        {
            await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
            return;
        }

        var objetoEditado = new Objeto()
        {
            IdObjeto = objetoActual.IdObjeto,
            IdInstituto = idInstituto,
            NombreObjeto = nombreObjeto,
            Descripcion = descripcion,
            TipoObjeto = tipoObjeto,
            CreadoPor = usuario,
            FechaDeCreacion = DateTime.Now,
            ModificadoPor = usuario,
            FechaDeModificacion = DateTime.Now
        };

        string objetoJson = System.Text.Json.JsonSerializer.Serialize(objetoEditado);
        ServicioObjeto servicioObjeto = new();
        bool registroExitoso = await servicioObjeto.ActualizarObjeto(objetoJson, objetoEditado);
        if (registroExitoso)
        {
            await DisplayAlert("Éxito", "Objeto actualizado correctamente", "OK");

            ServicioUsuario servicioUsuario = new();
            int userId = await servicioUsuario.ObtenerIdUsuario(usuario);

            await ServicioBitacora.AgregarRegistro(userId, 1, "Editó", "Objetos");
            
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al actualizar el objeto. Por favor, intenta nuevamente.", "OK");
        }
    }
}