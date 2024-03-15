using AMBEApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AMBEApp.ViewModels
{
    public class TipoViajeViewModel : INotifyPropertyChanged
    {
        private List<TipoViaje> _tipoViaje;


        public List<TipoViaje> TipoViajes
        {
            get => _tipoViaje;
            set
            {
                _tipoViaje = value;
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
