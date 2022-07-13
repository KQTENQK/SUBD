using DbM.MVVM.Model.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbM.MVVM.ViewModel.MenuControlsContent
{
    public abstract class ViewModelControlContent : INotifyPropertyChanged
    {
        private User _user;
        private string _dbName;

        public User User => _user;
        public string DbName => _dbName;

        public ViewModelControlContent(User user, string dbName)
        {
            _user = user;
            _dbName = dbName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
