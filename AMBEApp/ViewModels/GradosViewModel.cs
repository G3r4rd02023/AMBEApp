using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace AMBEApp.ViewModels
{
    public class GradosViewModel : INotifyPropertyChanged
    {
        private List<Grado> _grados;

        public List<Grado> Grados
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
