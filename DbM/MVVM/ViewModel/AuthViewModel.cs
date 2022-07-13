using DbM.Core.Command;
using DbM.Core.Data;
using DbM.MVVM.Model.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DbM.MVVM.ViewModel
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private Logger _logger = new Logger();
        private User _user;
        private UndefinedUser _undefinedUser = new UndefinedUser();
        private Command _auth;

        public Command Auth
        {
            get
            {
                return _auth ?? (_auth = new Command(TryAuth));
            }
        }

        public UndefinedUser UndefinedUser => _undefinedUser;

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action UserAuthorized;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void TryAuth(object sender)
        {
            if (IsInputDataCorrect())
                if (_logger.TryLogIn(_undefinedUser.Username, _undefinedUser.Password, _undefinedUser))
                {
                    if (_undefinedUser.SelectedDbName == null)
                        return;

                    _user = _logger.User;
                    var mainWindow = new MainWindow();
                    mainWindow.DataContext = new MainViewModel(_user, _undefinedUser.SelectedDbName);
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();
                    UserAuthorized?.Invoke();
                }
        }

        private bool IsInputDataCorrect()
        {
            if (_undefinedUser.Username == string.Empty || _undefinedUser.Password == string.Empty)
                return false;

            return true;
        }
    }
}
