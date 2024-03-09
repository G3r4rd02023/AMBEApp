using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMBEApp.ViewModels
{
    public class InstitutoViewModel : INotifyPropertyChanged
    {
        private List<Institutos> _institutos;

        public List<Institutos> Institutos
        {
            get => _institutos;
            set
            {
                _institutos = value;
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
