namespace AMBEApp.Services
{
    public class ServicioValidaciones
    {
        public static bool ValidarPicker(Picker picker)
        {
            if (picker.SelectedItem == null || string.IsNullOrEmpty(picker.SelectedItem.ToString()))
            {              
                return false;
            }
            return true;
        }

        public static bool ValidarEntradas(params string[] entradas)
        {
            foreach (var entrada in entradas)
            {
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    return false; 
                }
            }
            return true; 
        }
    }

}
