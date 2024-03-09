using AMBEApp.Models;
using AMBEApp.Services;
using Auth0.OidcClient;

namespace AMBEApp.Pages;

public partial class RegistroPage : ContentPage
{

    private readonly Auth0Client auth0Client;
    private readonly string _usuario;
    public RegistroPage(string usuario, Auth0Client client)
	{
		InitializeComponent();
        _usuario = usuario;
        auth0Client = client;
        CargarInstitutos();
        CargarTipoPersonas();
    }

    public RegistroPage()
    {
        InitializeComponent();
    }

    private async void CargarTipoPersonas()
    {
        try
        {
            ServicioTipoPersona servicioTipoPersona = new();
            var tipoPersonas = await servicioTipoPersona.ObtenerLista();
            pickerTipoPersona.ItemsSource = tipoPersonas.Select(r => r.TipoPersona).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar los tipos de persona: " + ex.Message, "OK");
        }
    }

    private async void CargarInstitutos()
    {
        try
        {
            ServicioInstituto servicioInstituto = new();
            List<Institutos> lista = await servicioInstituto.ObtenerLista();

            pickerInstituto.ItemsSource = lista.Select(r => r.NombreInstituto).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar los institutos: " + ex.Message, "OK");
        }
    }

    private async void RegistrarUsuario(object sender, EventArgs e)
    {

        try
        {            
            if (!ServicioValidaciones.ValidarPicker(pickerInstituto))
            {
                await DisplayAlert("Error", "Por favor, selecciona un instituto.", "OK");
                return;
            }

            if (!ServicioValidaciones.ValidarPicker(pickerTipoPersona))
            {
                await DisplayAlert("Error", "Por favor, selecciona un tipo de persona.", "OK");
                return;
            }
           
            string primerNombre = TxtPrimerNombre.Text;
            string segundoNombre = TxtSegundoNombre.Text;
            string primerApellido = TxtPrimerApellido.Text;
            string segundoApellido = TxtSegundoApellido.Text;
            string correoElectronico = TxtCorreo.Text;
            DateTime fechaNacimiento = DpFechaNacimiento.Date;
            string genero = ChkMasculino.IsChecked ? "Masculino" : "Femenino";
            ServicioRoles servicioRoles = new();
            int idRol = await servicioRoles.ObtenerIdRolPorNombre(pickerTipoPersona.SelectedItem.ToString());
            ServicioInstituto servicioInstituto = new();
            int idInstituto = await servicioInstituto.ObtenerIdInstitutoPorNombre(pickerInstituto.SelectedItem.ToString());
            ServicioTipoPersona servicioTipoPersona = new();
            int idTipoPersona = await servicioTipoPersona.ObtenerIdTipoPersonaPorNombre(pickerTipoPersona.SelectedItem.ToString());
            string usuario = _usuario;
           
            if (fechaNacimiento > DateTime.Today)
            {
                await DisplayAlert("Error", "La fecha no puede ser posterior a la fecha actual", "OK");
                return;
            }

            if (!ServicioValidaciones.ValidarEntradas(primerNombre, primerApellido, correoElectronico))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }
                        
            var persona = new Personas
            {
                PrimerNombre = primerNombre,
                SegundoNombre = segundoNombre,
                PrimerApellido = primerApellido,
                SegundoApellido = segundoApellido,
                FechaNacimiento = fechaNacimiento,
                Genero = genero,
                IdRol = idRol,
                IdInstituto = idInstituto,
                IdTipoPersona = idTipoPersona,
                Usuario = usuario,
                NombreUsuario = primerNombre + " " + primerApellido,
                CorreoElectronico = usuario,
                Contraseña = "hsdfgjhgfjbfxsl",
                Estado = "Nuevo",
                FechaUltimaConexion = DateTime.Now,
                CreadoPor = usuario,
                ModificadoPor = usuario
            };
            
            string personaJson = System.Text.Json.JsonSerializer.Serialize(persona);

            ServicioUsuario servicioUsuario = new();
            bool usuarioExiste = await servicioUsuario.UsuarioExiste(_usuario);
            if (usuarioExiste)
            {
                await DisplayAlert("Error", "El usuario ya está registrado.", "OK");
            }
            else
            {
                bool registroExitoso = await ServicioUsuario.RegistrarUsuario(personaJson);
                if (registroExitoso)
                {
                    await DisplayAlert("Éxito", "Usuario registrado correctamente, tu perfil necesita ser aprobado por el administrador", "OK");               
                    int idUsuario = await servicioUsuario.ObtenerIdUsuario(_usuario);
                    ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Registro", "Usuario");
                    await Navigation.PushAsync(new LoginPage(auth0Client));
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al registrar el usuario. Por favor, intenta nuevamente.", "OK");
                }
            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Ha ocurrido un error : {ex.Message}", "OK");
            return;
        }

    }
}