using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages;

public partial class CrearInstitutoPage : ContentPage
{
	public CrearInstitutoPage()
	{
		InitializeComponent();
	}

    private async void CrearInstituto(object sender, EventArgs e)
    {
        try
        {
           
            string nombreInstituto = TxtNombreInstituto.Text;
            string RTN = TxtRTN.Text;
            string telefono = TxtTelefono.Text;
            string direccion = TxtDireccion.Text;
            string email = TxtEmail.Text;
            string descripcion = TxtDescripcion.Text;
           
            var username = ServicioUsuario.UsuarioAutenticado;


            if (!ServicioValidaciones.ValidarEntradas(nombreInstituto,RTN,telefono,direccion,email,descripcion))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var instituto = new Institutos()
            {
                NombreInstituto = nombreInstituto,
                Rtn = RTN,
                Telefono = telefono,
                Direccion = direccion,
                Email = email,
                Descripcion = descripcion,
                CreadoPor = username,
                FechaCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaModificacion = DateTime.Now
            };

            string institutoJson = System.Text.Json.JsonSerializer.Serialize(instituto);


            ServicioInstituto servicioInstituto = new();
            bool institutoExiste = await servicioInstituto.InstitutoExiste(nombreInstituto);

            if (institutoExiste)
            {
                await DisplayAlert("Error", "El instituto ya está existe.", "OK");
            }
            else
            {                
                var httpClient = new HttpClient();
                var content = new StringContent(institutoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Institutos", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Instituto creado correctamente, pero aun necesita ser aprobado por el administrador"
                        , "OK");
                    //ServicioUsuario servicioUsuario = new();
                    //int idUsuario = await servicioUsuario.ObtenerIdUsuario(username!);
                    //ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Roles");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al crear el instituto. Por favor, intenta nuevamente.", "OK");
                }

            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Por favor complete todos los campos : {ex.Message}", "OK");
            return;
        }

    }
}