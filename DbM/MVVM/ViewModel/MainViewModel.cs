using DbM.MVVM.Model.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbM.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private User _user;
        private string _dbName;
        private object _currentContentView;

        public User User => _user;
        public string DbName => _dbName;

        public object CurrentContentView
        {
            get
            {
                return _currentContentView;
            }

            set
            {
                _currentContentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel() { }

        public MainViewModel(User user, string dbName)
        {
            _user = user;
            _dbName = dbName;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
