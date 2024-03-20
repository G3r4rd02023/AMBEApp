using AMBEApp.Services;

namespace AMBEApp.Pages;

public partial class PerfilPage : ContentPage
{
	
    public PerfilPage()
    {
        InitializeComponent();
        var usuario = ServicioUsuario.UsuarioAutenticado;
        var foto = ServicioUsuario.ImagenUsuario;
        UsernameLbl.Text = usuario;
        UserPictureImg.Source = foto;
    }

   
}