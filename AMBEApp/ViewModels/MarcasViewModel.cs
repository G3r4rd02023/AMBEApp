using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMBEApp.ViewModels
{
    public class MarcasViewModel : INotifyPropertyChanged
    {

        private List<Marca> _marcas;

        public List<Marca> Marcas
        {
            get => _marcas;
            set
            {
                _marcas = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
