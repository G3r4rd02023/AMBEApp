using AMBEApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMBEApp.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private bool _esAdmin;
        private bool _esAdminInstituto;
        private bool _esEmpleado;
        private bool _esCliente;
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

        public bool EsAdminInstituto
        {
            get => _esAdminInstituto;
            set
            {
                if (_esAdminInstituto != value)
                {
                    _esAdminInstituto = value;
                    OnPropertyChanged(nameof(EsAdminInstituto));
                }
            }
        }

        public bool EsEmpleado
        {
            get => _esEmpleado;
            set
            {
                if (_esEmpleado != value)
                {
                    _esEmpleado = value;
                    OnPropertyChanged(nameof(EsEmpleado));
                }
            }
        }

        public bool EsCliente
        {
            get => _esCliente;
            set
            {
                if (_esCliente != value)
                {
                    _esCliente = value;
                    OnPropertyChanged(nameof(EsCliente));
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
            EsAdmin = await servicioUsuario.VerificarRol(1);
            EsAdminInstituto = await servicioUsuario.VerificarRol(2);
            EsCliente = await servicioUsuario.VerificarRol(3);
            EsEmpleado = await servicioUsuario.VerificarRol(4) || await servicioUsuario.VerificarRol(5);
        }
    }
}
