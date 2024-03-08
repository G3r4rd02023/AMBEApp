
using AMBEApp.Pages;
using Auth0.OidcClient;

namespace AMBEApp
{
    public partial class AppShell : Shell
    {
        private readonly Auth0Client auth0Client;
        public AppShell(Auth0Client client)
        {
            auth0Client = client;
            InitializeComponent();            
        }

        private async void CerrarSesion_Clicked(object sender, EventArgs e)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea salir?", "Si, continuar", "No, volver");
            if (answer)
            {
                App.Current.MainPage = new LoginPage(auth0Client);
                await Navigation.PushAsync(new LoginPage(auth0Client));
            }
        }
    }
}
