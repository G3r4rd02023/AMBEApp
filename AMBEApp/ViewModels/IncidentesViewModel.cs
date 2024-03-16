using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMBEApp.ViewModels
{
    internal class IncidentesViewModel : INotifyPropertyChanged

    {
        private List<Incidentes> _incidentes;

        public List<Incidentes> Incidentes
        {
            get => _incidentes;
            set
            {
                _incidentes = value;
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
