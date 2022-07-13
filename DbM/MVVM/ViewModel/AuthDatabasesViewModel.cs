using DbM.Core.Command;
using DbM.Core.Data;
using DbM.MVVM.Model.Data;
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
    public class AuthDatabasesViewModel : INotifyPropertyChanged
    {
        private UndefinedUser _user;
        private DatabaseModel _selected;
        private Logger _logger = new Logger();
        private Command _select;

        public Command Select
        {
            get
            {
                return _select ?? (_select = new Command(obj => { SelectButtonClick?.Invoke(); }));
            }
        }

        public ObservableCollection<DatabaseModel> Databases { get; set; }

        public DatabaseModel Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                _user.SelectedDbName = _selected.Name;
                OnPropertyChanged("Selected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action SelectButtonClick;

        public AuthDatabasesViewModel(UndefinedUser user)
        {
            Databases = new ObservableCollection<DatabaseModel>();
            _user = user;
            ObservableCollection<string> databasesNames = _logger.LoadDatabases(_user.Username, _user.Password);

            foreach (var databaseName in databasesNames)
            {
                Databases.Add(new DatabaseModel { Name = databaseName });
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
