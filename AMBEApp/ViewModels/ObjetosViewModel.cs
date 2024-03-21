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
    public class ObjetosViewModel : INotifyPropertyChanged
    {
        private List<Objeto> _objetos;

        public List<Objeto> Objetos
        {
            get => _objetos;
            set
            {
                _objetos = value;
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
