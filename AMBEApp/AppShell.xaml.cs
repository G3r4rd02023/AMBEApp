
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

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            if (args.Source == ShellNavigationSource.ShellItemChanged)
            {
                try
                {
                    base.OnNavigating(args);
                }
                catch (System.MissingMethodException ex)
                {
                    // Manejar la excepción aquí
                    Console.WriteLine("Se produjo una excepción durante la navegación a la página: " + ex.Message);

                    // Detener la navegación para evitar que se lance la excepción
                    args.Cancel();
                }
            }
        }
    }
}
