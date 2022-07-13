using DbM.Core.Data;
using DbM.MVVM.Model.Menu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbM.MVVM.Model.User
{
    public abstract class User
    {
        public string Name { get; protected set; }
        public string Password { get; protected set; }
        public DataHandler DataHandler { get; protected set; }
        public ObservableCollection<MenuItemModel> MenuItems { get; protected set; }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
            MenuItems = new ObservableCollection<MenuItemModel>();
            MenuItems.Add(new TablesObservingMenuItemModel());
        }
    }
}
