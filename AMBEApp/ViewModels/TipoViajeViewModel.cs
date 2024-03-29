﻿using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
