namespace AMBEApp.Pages.Unidades;

public partial class UnidadesPage : ContentPage
{
	public UnidadesPage()
	{
		InitializeComponent();
	}


    private void OnGenerarPdfClicked(object sender, EventArgs e)
    {
    }

    private void OnImprimirClicked(object sender, EventArgs e)
    {
    }

    private void OnEliminarClicked(object sender, EventArgs e)
    {

    }

    private void OnSearchIconTapped(object sender, EventArgs e)
    {
        DisplayAlert("Búsqueda", "Realizar búsqueda...", "Aceptar");
    }

    private void OnCrearNuevoRegistroClicked(object sender, EventArgs e)
    {
        DisplayAlert("Nuevo Registro", "Implementa la lógica para crear un nuevo registro.", "Aceptar");
    }

    private void OnEditarClicked(object sender, EventArgs e)
    {

    }
}