using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace AMBEApp.ViewModels
{
    public class GradosViewModel : INotifyPropertyChanged
    {
        private List<Grados> _grados;

        public List<Grados> Grados
        {
            get => _grados;
            set
            {
                _grados = value;
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
