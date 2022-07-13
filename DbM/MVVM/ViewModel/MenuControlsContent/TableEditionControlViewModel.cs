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
using System.Windows.Controls;

namespace DbM.MVVM.ViewModel.MenuControlsContent
{
    public class TableEditionControlViewModel : ViewModelControlContent
    {
        public ObservableCollection<TableModel> TableSource { get; set; }
        public ObservableCollection<DatabaseModel> DatabasesSource { get; set; }

        public TableEditionControlViewModel(User user, string dbName) : base(user, dbName)
        {
            TableSource = new ObservableCollection<TableModel>();
            DatabasesSource = new ObservableCollection<DatabaseModel>();

            LoadDatabasesAsync();
            LoadTablesAsync();
        }

        public async void LoadDatabasesAsync()
        {
            await Task.Run(() => LoadDatabases());
        }

        public async void LoadTablesAsync()
        {
            await Task.Run(() => LoadTables());
        }

        public async void LoadTablesAsync(string dbName)
        {
            await Task.Run(() => LoadTables(dbName));
        }

        public void LoadDatabases()
        {
            ObservableCollection<string> databasesNames = User.DataHandler.LoadDbNames();

            App.Current.Dispatcher.Invoke(() => DatabasesSource.Clear());

            foreach (var dbName in databasesNames)
            {
                App.Current.Dispatcher.Invoke(() => DatabasesSource.Add(new DatabaseModel { Name = dbName }));
            }
        }

        public void LoadTables()
        {
            ObservableCollection<string> tableNames = User.DataHandler.LoadTableNames(DbName);

            App.Current.Dispatcher.Invoke(() => TableSource.Clear());

            foreach (var dbName in tableNames)
            {
                App.Current.Dispatcher.Invoke(() => TableSource.Add(new TableModel { Name = dbName }));
            }
        }

        public void LoadTables(string dbName)
        {
            ObservableCollection<string> tableNames = User.DataHandler.LoadTableNames(dbName);

            App.Current.Dispatcher.Invoke(() => TableSource.Clear());

            foreach (var tableName in tableNames)
            {
                App.Current.Dispatcher.Invoke(() => TableSource.Add(new TableModel { Name = dbName }));
            }
        }
    }
}
