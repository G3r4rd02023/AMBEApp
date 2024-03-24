using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages.Personas;

public partial class AlumnosPage : ContentPage
{
	public AlumnosPage()
	{
		InitializeComponent();
	}

    private async void CrearAlumno(object sender, EventArgs e)
    {
        try
        {

            var username = ServicioUsuario.UsuarioAutenticado;
            ServicioUsuario servicioUsuario = new();
            var usuarios = await servicioUsuario.ObtenerLista();
            var usuarioEncontrado = usuarios.FirstOrDefault(u => u.Usuario == username);

            
            int idInstituto = usuarioEncontrado.IdInstituto;
            string primerNombre = TxtPrimerNombre.Text;
            string segundoNombre = TxtSegundoNombre.Text;
            string primerApellido = TxtPrimerApellido.Text;
            string segundoApellido = TxtSegundoApellido.Text;
            DateTime fechaNacimiento = DpFechaNacimiento.Date;
            string genero = ChkMasculino.IsChecked ? "Masculino" : "Femenino";



            if (!ServicioValidaciones.ValidarEntradas(primerNombre,segundoNombre,primerApellido,segundoApellido))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            if (fechaNacimiento > DateTime.Today)
            {
                await DisplayAlert("Error", "La fecha no puede ser posterior a la fecha actual", "OK");
                return;
            }

            var nuevoAlumno = new Alumno()
            {
                IdTipoPersona = 3,
                IdInstituto = idInstituto,
                PrimerNombre = primerNombre,
                SegundoNombre = segundoNombre,
                PrimerApellido = primerApellido,
                SegundoApellido = segundoApellido,
                FechaNacimiento = fechaNacimiento,
                Genero = genero,
                CreadoPor = username,
                FechaCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaModificacion = DateTime.Now
            };

            string alumnoJson = System.Text.Json.JsonSerializer.Serialize(nuevoAlumno);
                      
            var httpClient = new HttpClient();
            var content = new StringContent(alumnoJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Personas", content);
            if (response.IsSuccessStatusCode)
            {
              await DisplayAlert("Éxito", "Alumno creado correctamente", "OK");

              int idUsuario = await servicioUsuario.ObtenerIdUsuario(username!);
              await ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Personas");
              await Navigation.PopAsync();
            }
            else
            {
              await DisplayAlert("Error", "Hubo un problema al crear el alumno. Por favor, intenta nuevamente.", "OK");
            }            
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Por favor complete todos los campos : {ex.Message}", "OK");
            return;
        }

    }

}