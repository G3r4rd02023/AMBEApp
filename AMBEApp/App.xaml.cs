using AMBEApp.Pages;
using Auth0.OidcClient;
namespace AMBEApp
{

    public partial class App : Application
    {
        private readonly Auth0Client auth0Client;

        public App(Auth0Client client)
        {
            InitializeComponent();
            auth0Client = client;
            MainPage = new LoginPage(auth0Client);

        }

    }
}
