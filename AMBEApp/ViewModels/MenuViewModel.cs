using AMBEApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AMBEApp.ViewModels
{
    public class MenuViewModel: INotifyPropertyChanged
    {
        private bool _esAdmin;
        public bool EsAdmin
        {
            get => _esAdmin;
            set
            {
                if (_esAdmin != value)
                {
                    _esAdmin = value;
                    OnPropertyChanged(nameof(EsAdmin));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task VerificarRolAsync()
        {
            ServicioUsuario servicioUsuario = new();
            EsAdmin = await servicioUsuario.EsAdmin();
        }
    }
}
