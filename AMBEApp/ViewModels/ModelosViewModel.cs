using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMBEApp.ViewModels
{
    public class ModelosViewModel : INotifyPropertyChanged
    {

        private List<Modelos> _modelos;

        public List<Modelos> Modelos
        {
            get => _modelos;
            set
            {
                _modelos = value;
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
