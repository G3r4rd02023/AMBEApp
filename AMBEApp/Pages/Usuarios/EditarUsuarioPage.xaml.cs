using AMBEApp.Models;
using AMBEApp.Services;

namespace AMBEApp.Pages;

public partial class EditarUsuarioPage : ContentPage
{
    public Usuarios Usuario { get; set; }
    public EditarUsuarioPage(Usuarios usuario)
    {
        InitializeComponent();
        Usuario = usuario;
        pickerEstado.Items.Add("Nuevo");
        pickerEstado.Items.Add("Activo");
        pickerEstado.Items.Add("Bloqueado");
        pickerEstado.Items.Add("Inactivo");
        pickerEstado.SelectedIndex = 0;
        CargarRoles();
        TxtUsuario.Text = usuario.Usuario ?? string.Empty;
        TxtNombreUsuario.Text = usuario.NombreUsuario ?? string.Empty;
        TxtCorreo.Text = usuario.CorreoElectronico ?? string.Empty;
        TxtFechaUltimaConexion.Text = usuario.FechaUltimaConexion.ToString("dd/MM/yyyy") ?? string.Empty;
    }

    private async void CargarRoles()
    {
        try
        {
            ServicioRoles servicioRoles = new();
            var roles = await servicioRoles.ObtenerLista();
            pickerRol.ItemsSource = roles.Select(r => r.Descripcion).ToList();
            var usuario = Usuario;
            TxtRol.Text = await servicioRoles.ObtenerNombreRol(usuario.IdRol);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar los roles: " + ex.Message, "OK");
        }
    }

    private async void EditarUsuario(object sender, EventArgs e)
    {
        try
        {
            if (pickerRol.SelectedItem == null || string.IsNullOrEmpty(pickerRol.SelectedItem.ToString()))
            {
                await DisplayAlert("Error", "Por favor, selecciona un rol.", "OK");
            }

            int idUsuario = Usuario.IdUsuario;
            int idPersona = Usuario.IdPersona;
            string usuario = Usuario.Usuario;
            int idInstituto = Usuario.IdInstituto;
            string nombreUsuario = Usuario.NombreUsuario;
            string correoElectronico = Usuario.CorreoElectronico;
            string Estado = Usuario.Estado;
            int idRol = Usuario.IdRol;
            DateTime fechaUltimaConexion = Usuario.FechaUltimaConexion;

            ServicioRoles servicioRoles = new();
            int nuevoIdRol = await servicioRoles.ObtenerIdRolPorNombre(pickerRol.SelectedItem.ToString());
            string nuevoEstado = pickerEstado.SelectedItem.ToString();

            Usuarios user = new()
            {
                IdUsuario = idUsuario,
                IdPersona = idPersona,
                Usuario = usuario,
                IdInstituto = idInstituto,
                NombreUsuario = nombreUsuario,
                Contraseña = "dg2do7xbxjksjs",
                CorreoElectronico = correoElectronico,
                Estado = nuevoEstado,
                IdRol = nuevoIdRol,
                FechaUltimaConexion = fechaUltimaConexion,
                CreadoPor = Usuario.CreadoPor,
                FechaCreacion = Usuario.FechaCreacion,
                ModificadoPor = "ADMINISTRADOR",
                FechaModificacion = DateTime.Now
            };

            string userJson = System.Text.Json.JsonSerializer.Serialize(user);
            ServicioUsuario servicioUsuario = new();
            bool registroExitoso = await servicioUsuario.ActualizarUsuario(userJson, user);

            if (registroExitoso)
            {
                await DisplayAlert("Éxito", "Usuario actualizado correctamente", "OK");

                int userId = await servicioUsuario.ObtenerIdUsuario(usuario);

                //ServicioBitacora.AgregarRegistro(userId, 1, "Editó", "Usuarios");
                // Aquí puedes manejar la navegación de regreso o a otra página si es necesario
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Hubo un problema al actualizar el usuario. Por favor, intenta nuevamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ha ocurrido un error: {ex.Message}", "OK");
        }
    }
}