using Auth0.OidcClient;

namespace AMBEApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly Auth0Client auth0Client;

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
            //string nombreUsuario = loginResult.User.Identity.Name;
            //ImageSource imagenUsuario = ImageSource.FromUri(new Uri(loginResult.User.Claims
            //.FirstOrDefault(c => c.Type == "picture")?.Value));
            //await Navigation.PushAsync(new PerfilPage(nombreUsuario, imagenUsuario));
            App.Current.MainPage = new AppShell(auth0Client);          
            await Navigation.PushAsync(new HomePage());

            //bool isFirstLogin = await servicioUsuario.ValidarPrimerLogin(usuario!);

            //if (isFirstLogin)
            //{

            //    await Navigation.PushAsync(new RegistroPage(usuario!, auth0Client));               
            //    Shell.SetNavBarIsVisible(this, true);

            //}
            //else
            //{
            //    LoginView.IsVisible = false;
            //    HomeView.IsVisible = true;
            //    Shell.SetNavBarIsVisible(this, true);
            //    //NavigationPage.SetHasNavigationBar(this, true);
            //    //App.Current.MainPage = new AppShell();
            //}

            //int idUsuario = await ServicioRoles.ObtenerIdUsuario(usuario!);
            //ServicioBitacora.AgregarRegistro(idUsuario, 1, "Inicio Sesión", "Sistema");
        }
        else
        {
            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        }

    }

   
}