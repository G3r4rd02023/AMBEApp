using AMBEApp.Models;
using AMBEApp.Services;
using Auth0.OidcClient;

namespace AMBEApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly Auth0Client auth0Client;
    public static string UsuarioAutenticado { get; set; }
    public LoginPage(Auth0Client client)
	{
		InitializeComponent();
        auth0Client = client;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginResult = await auth0Client.LoginAsync();

        if (!loginResult.IsError)
        {
            string nombreUsuario = loginResult.User.Identity.Name;
            UsuarioAutenticado = nombreUsuario;
            ServicioUsuario.SetUsuarioAutenticado(nombreUsuario);          
            ImageSource imagenUsuario = ImageSource.FromUri(new Uri(loginResult.User.Claims
            .FirstOrDefault(c => c.Type == "picture")?.Value));
            ServicioUsuario.SetImagenUsuario(imagenUsuario);
            //await Navigation.PushAsync(new PerfilPage(nombreUsuario, imagenUsuario));                                
            ServicioUsuario servicioUsuario = new();
            bool isFirstLogin = await servicioUsuario.ValidarPrimerLogin(nombreUsuario);

            if (isFirstLogin)
            {
                App.Current.MainPage = new AppShell(auth0Client);               
                AppShell.Current.FlyoutIsPresented = true;
                var registroPage = new RegistroPage(nombreUsuario, auth0Client);                
                await Shell.Current.Navigation.PushAsync(registroPage);
                //await Shell.Current.GoToAsync("//RegistroPage");
                //await Navigation.PushAsync(new RegistroPage(nombreUsuario, auth0Client));                                      
            }
            else
            {
                bool usuarioActivo = await servicioUsuario.ValidarUsuarioActivo(nombreUsuario);
                if(usuarioActivo)
                {
                    App.Current.MainPage = new AppShell(auth0Client);
                    await Shell.Current.GoToAsync("//HomePage");
                    //await Navigation.PushAsync(new HomePage());    
                }
                else
                {
                    await DisplayAlert("Error", "Tu solicitud aun no ha sido aprobada por el administrador, por favor intenta mas tarde", "Ok");
                }
            }
            ServicioInstituto servicioInstituto = new();
            int idUsuario = await servicioUsuario.ObtenerIdUsuario(nombreUsuario);
            int idInstituto = await servicioInstituto.ObtenerIdInstituto(nombreUsuario);
            await ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Inicio Sesión", "Sistema");
        }
        else
        {
            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        }
    }

   
}