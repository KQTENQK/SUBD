using DbM.Core.Data;
using DbM.MVVM.Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbM.MVVM.Model.User
{
    public class DbEditor : User
    {
        public string SelectedDbName { get; private set; }

        public DbEditor(string login, string password, string dbName) : base(login, password)
        {
            SelectedDbName = dbName;

            DataHandler = new DataHandler(this);
            MenuItems.Add(new TableEditionMenuItemModel());
        }
    }
}
