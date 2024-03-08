namespace AMBEApp.Pages;

public partial class PerfilPage : ContentPage
{
	
    public PerfilPage()
    {
        InitializeComponent();
    }

    public PerfilPage(string userName, ImageSource userPicture)
	{
		InitializeComponent();
        UsernameLbl.Text = userName;
        UserPictureImg.Source = userPicture;
    }
}