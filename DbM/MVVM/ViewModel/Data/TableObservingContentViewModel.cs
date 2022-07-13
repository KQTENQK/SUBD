using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Data;
using DbM.MVVM.Model.User;

namespace DbM.MVVM.ViewModel.Data
{
    public class TableObservingContentViewModel : INotifyPropertyChanged
    {
        private DataTable _currentDataTable = new DataTable();
        private User _user;
        private string _dbName;
        private string _tableName;
        private ObservableCollection<string> _columnNames = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> ColumnNames => _columnNames;

        public DataView CurrentDataTable => _currentDataTable.DefaultView;

        public TableObservingContentViewModel(User user, string dbName, string tableName)
        {
            _user = user;
            _dbName = dbName;
            _tableName = tableName;
            LoadDatabaseModelAsync();
        }

        public void LoadDatabaseModel()
        {
           App.Current.Dispatcher.Invoke(() => _currentDataTable = _user.DataHandler.GetDataSource(_dbName, _tableName));

            foreach (DataColumn column in _currentDataTable.Columns)
            {
                App.Current.Dispatcher.Invoke(() => _columnNames.Add(column.ColumnName));
            }
        }
        public async void LoadDatabaseModelAsync()
        {
            await Task.Run(() => LoadDatabaseModel());
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
