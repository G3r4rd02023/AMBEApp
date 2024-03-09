using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMBEApp.ViewModels
{
    public class BitacoraViewModel : INotifyPropertyChanged
    {
        private List<Bitacora> _bitacora;

        public List<Bitacora> Bitacoras
        {
            get => _bitacora;
            set
            {
                _bitacora = value;
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
