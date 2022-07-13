using DbM.MVVM.Model.User;
using DbM.MVVM.View;
using DbM.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbM.Core.Data
{
    public class Logger
    {
        private const string SysadminServerRole = "sysadmin";
        private const string DbcreatorServerRole = "securityadmin";
        private const string DbWriterRole = "db_datawriter";
        private const string DbReaderRole = "db_datareader";

        private SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
        private User _user;

        public User User => _user;

        public Logger()
        {
            string dataSource = ConfigurationManager.AppSettings.GetValues("serverInstance").FirstOrDefault().ToString();

            _sqlConnectionStringBuilder.DataSource = dataSource;
            _sqlConnectionStringBuilder.IntegratedSecurity = false;
            _sqlConnectionStringBuilder.InitialCatalog = string.Empty;
        }

        public bool TryLogIn(string login, string password, UndefinedUser user)
        {
            _sqlConnectionStringBuilder.UserID = login;
            _sqlConnectionStringBuilder.Password = password;

            if (TryConnect())
            {
                //if (TryInitServerMember())
                //    return true;

                if (TryInitDbRegularMember(user))
                    return true;
            }

            return false;
        }

        private bool TryInitServerMember()
        {
            List<string> serverRoles = GetServerRoles(_sqlConnectionStringBuilder.UserID);

            string userServerType = string.Empty;

            if (serverRoles.Count == 0)
                return false;
            //var dbViewWindow = new DatabaseViewWindow(_sqlConnectionStringBuilder.UserID, _sqlConnectionStringBuilder.Password);
            //dbViewWindow.DatabaseSelected += OnDatabaseSelectedByPublic;
            //dbViewWindow.ShowDialog();


            if (serverRoles.Contains(SysadminServerRole))
            {
                _user = new SuperUser(_sqlConnectionStringBuilder.UserID, _sqlConnectionStringBuilder.Password, null);
                return true;
            }

            if (serverRoles.Contains(DbcreatorServerRole))
            {
                _user = new DbEditor(_sqlConnectionStringBuilder.UserID, _sqlConnectionStringBuilder.Password, null);
                return true;
            }

            return true;
        }

        public ObservableCollection<string> LoadDatabases(string login, string password, Func<string, bool> filterFunc = null)
        {
            string query = "SELECT name FROM sys.databases;";
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            string dataSource = ConfigurationManager.AppSettings.GetValues("serverInstance").FirstOrDefault().ToString();

            sqlConnectionStringBuilder.DataSource = dataSource;
            sqlConnectionStringBuilder.IntegratedSecurity = false;
            sqlConnectionStringBuilder.InitialCatalog = string.Empty;
            sqlConnectionStringBuilder.Password = password;
            sqlConnectionStringBuilder.UserID = login;

            ObservableCollection<string> dbNames = new ObservableCollection<string>();

            using (SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string currentDbName = reader["name"].ToString();

                        if (filterFunc != null && filterFunc(currentDbName))
                            continue;

                        dbNames.Add(currentDbName);
                    }

                    reader.Close();

                    command.Connection.Close();
                }
                catch (Exception exception)
                {
                    //new ErrorWindow(exception.Message).ShowDialog();
                }
            }

            return dbNames;
        }

        private bool TryInitDbRegularMember(UndefinedUser user)
        {
            var dbSelectionWindow = new AuthDatabasesWindow();
            var context = new AuthDatabasesViewModel(user);
            dbSelectionWindow.DataContext = context;
            dbSelectionWindow.ShowDialog();
            //Task.WaitAll(() => )


            List<string> roles = GetDbRoles(user.SelectedDbName);
            List<string> serverRoles = GetServerRoles(_sqlConnectionStringBuilder.UserID);

            if (serverRoles.Contains(SysadminServerRole))
            {
                _user = new SuperUser(_sqlConnectionStringBuilder.UserID, _sqlConnectionStringBuilder.Password, user.SelectedDbName);
                return true;
            }

            if (serverRoles.Contains(DbcreatorServerRole))
            {
                _user = new DbEditor(_sqlConnectionStringBuilder.UserID, _sqlConnectionStringBuilder.Password, user.SelectedDbName);
                return true;
            }

            if (roles.Contains(DbWriterRole))
            {
                _user = new DbWriter(_sqlConnectionStringBuilder.UserID, _sqlConnectionStringBuilder.Password, user.SelectedDbName);
                return true;
            }

            if (roles.Contains(DbReaderRole))
            {
                _user = new DbReader(_sqlConnectionStringBuilder.UserID, _sqlConnectionStringBuilder.Password, user.SelectedDbName);
                return true;
            }

            return false;
        }

        private List<string> GetDbRoles(string dbName)
        {
            List<string> roles = new List<string>();

            using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    string query = $"USE {dbName}; EXEC sp_helpuser '{_sqlConnectionStringBuilder.UserID}';";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                        roles.Add(reader["RoleName"].ToString());

                    reader.Close();

                    command.Connection.Close();
                }
                catch { }
            }

            return roles;
        }

        private List<string> GetServerRoles(string login)
        {
            List<string> roles = new List<string>();

            using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                string query = $"EXEC sp_helpsrvrolemember;";

                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    if (reader["MemberName"].ToString() == login)
                        roles.Add(reader["ServerRole"].ToString());

                reader.Close();

                command.Connection.Close();
            }

            return roles;
        }

        private bool TryConnect()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    connection.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
