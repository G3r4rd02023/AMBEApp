using AMBEApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMBEApp.ViewModels
{
    public class ParentescosViewModel : INotifyPropertyChanged
    {
        private List<Parentesco> _parentescos;


        public List<Parentesco> Parentescos
        {
            get => _parentescos;
            set
            {
                _parentescos = value;
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
