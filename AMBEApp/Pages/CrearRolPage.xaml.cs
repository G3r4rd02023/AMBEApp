using AMBEApp.Models;
using AMBEApp.Services;
using System.Text;

namespace AMBEApp.Pages;

public partial class CrearRolPage : ContentPage
{
    public CrearRolPage()
    {
        InitializeComponent();
        CargarInstitutos();
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

    private async void CrearRol(object sender, EventArgs e)
    {
        try
        {
            if (!ServicioValidaciones.ValidarPicker(pickerInstituto))
            {
                await DisplayAlert("Error", "Por favor, selecciona un instituto.", "OK");
                return;
            }

            string descripcion = TxtDescripcion.Text;
            ServicioInstituto servicioInstituto = new();
            int idInstituto = await servicioInstituto.ObtenerIdInstitutoPorNombre(pickerInstituto.SelectedItem.ToString());
            var username = ServicioUsuario.UsuarioAutenticado;


            if (!ServicioValidaciones.ValidarEntradas(descripcion))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var rol = new Roles()
            {
                IdInstituto = idInstituto,
                Descripcion = descripcion,
                CreadoPor = username,
                FechaCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaModificacion = DateTime.Now
            };

            string rolJson = System.Text.Json.JsonSerializer.Serialize(rol);


            ServicioRoles servicioRoles = new();
            bool rolExiste = await servicioRoles.RolExiste(descripcion);

            if (rolExiste)
            {
                await DisplayAlert("Error", "El rol ya est� existe.", "OK");
            }
            else
            {
                // Enviar los datos del rol a trav�s de una API
                var httpClient = new HttpClient();
                var content = new StringContent(rolJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Roles", content);
                
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("�xito", "Rol creado correctamente, pero aun necesita ser aprobado por el administrador"
                        , "OK");
                    ServicioUsuario servicioUsuario = new();
                    //int idUsuario = await servicioUsuario.ObtenerIdUsuario(username!);
                    //ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Roles");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al crear el rol. Por favor, intenta nuevamente.", "OK");
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