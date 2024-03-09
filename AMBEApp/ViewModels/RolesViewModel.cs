using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMBEApp.ViewModels
{
    public class RolesViewModel : INotifyPropertyChanged
    {
        private List<Roles> _roles;

        public List<Roles> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
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
