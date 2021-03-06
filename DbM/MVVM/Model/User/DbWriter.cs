using DbM.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbM.MVVM.Model.User
{
    public class DbWriter : User
    {
        public string SelectedDbName { get; private set; }

        public DbWriter(string login, string password, string dbName) : base(login, password)
        {
            SelectedDbName = dbName;
            DataHandler = new DataHandler(this);
        }
    }
}
