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
            var navPage = new NavigationPage(new LoginPage(auth0Client))
            {
                BarBackgroundColor = Colors.Purple,
                BarTextColor = Colors.White
            };
            MainPage = navPage;

        }

    }
}
