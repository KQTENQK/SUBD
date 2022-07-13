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

namespace DbM.MVVM.ViewModel.MenuControlsContent
{
    public class TablesObservingControlViewModel : ViewModelControlContent
    {
        public ObservableCollection<TableModel> ListViewSource { get; set; }

        public TablesObservingControlViewModel(User user, string dbName) : base(user, dbName)
        {
            ListViewSource = new ObservableCollection<TableModel>();

            LoadTablesAsync();
        }

        public void LoadTables()
        {
            ObservableCollection<string> tableNames = User.DataHandler.LoadTableNames(DbName);

            foreach (var tableName in tableNames)
            {
                App.Current.Dispatcher.Invoke(() => ListViewSource.Add(new TableModel { Name = tableName }));
            }
        }

        public async void LoadTablesAsync()
        {
            await Task.Run(() => LoadTables());
        }
    }
}
