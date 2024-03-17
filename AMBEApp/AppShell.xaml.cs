using AMBEApp.Pages;
using AMBEApp.Services;
using AMBEApp.ViewModels;
using Auth0.OidcClient;

namespace AMBEApp
{
    public partial class AppShell : Shell
    {
        private readonly Auth0Client auth0Client;
        public MenuViewModel ViewModel { get; set; }
        public AppShell(Auth0Client client)
        {
            auth0Client = client;
            InitializeComponent();
            ViewModel = new MenuViewModel(); // Instancia de tu ViewModel
            BindingContext = ViewModel;
            ConfirmarRol();
        }

        private async void ConfirmarRol()
        {
            var viewModel = (MenuViewModel)BindingContext;
            ServicioUsuario servicioUsuario = new();
            viewModel.EsAdmin = await servicioUsuario.VerificarRol(1);
            viewModel.EsAdminInstituto = await servicioUsuario.VerificarRol(2);
            viewModel.EsCliente = await servicioUsuario.VerificarRol(3);
            viewModel.EsEmpleado = await servicioUsuario.VerificarRol(4) || await servicioUsuario.VerificarRol(5);
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

