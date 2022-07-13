using DbM.MVVM.Model.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DbM.Core.Data
{
    public class DataHandler
    {
        private SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder();

        public SqlConnectionStringBuilder SqlConnectionStringBuilder => _sqlConnectionStringBuilder;

        public DataHandler(User user)
        {
            string dataSource = ConfigurationManager.AppSettings.GetValues("serverInstance").FirstOrDefault().ToString();

            SqlConnectionStringBuilder.DataSource = dataSource;
            SqlConnectionStringBuilder.IntegratedSecurity = false;
            SqlConnectionStringBuilder.UserID = user.Name;
            SqlConnectionStringBuilder.Password = user.Password;
            SqlConnectionStringBuilder.InitialCatalog = string.Empty;
        }

        public ObservableCollection<string> LoadTableNames(string dbName)
        {
            string query = $"SELECT name FROM {dbName}.sys.Tables;";
            ObservableCollection<string> tableNames = new ObservableCollection<string>();

            using (SqlConnection connection = new SqlConnection(SqlConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                        tableNames.Add(reader["name"].ToString());

                    reader.Close();

                    command.Connection.Close();
                }
                catch (Exception exception)
                {
                    //(exception.Message).ShowDialog(); todo
                }
            }

            return tableNames;
        }

        public void UpdateRemoteDatabase(string dbName, string tableName, DataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                string query = $"USE {dbName}; SELECT * FROM {tableName};";

                connection.Open();
                DataTable remoteDataTable = new DataTable();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(remoteDataTable);

                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
                sqlDataAdapter.Update(dataTable);

                connection.Close();
            }
        }

        public DataTable GetDataSource(string dbName, string tableName)
        {
            DataTable dataTable = new DataTable();

            string query = $"USE {dbName}; SELECT * FROM {tableName};";

            using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

                sqlDataAdapter.Fill(dataTable);
                connection.Close();
            }

            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"] };

            return dataTable;
        }

        public ObservableCollection<string> LoadDbNames(Func<string, bool> filterFunc = null)
        {
            string query = "SELECT name FROM sys.databases;";
            ObservableCollection<string> dbNames = new ObservableCollection<string>();

            using (SqlConnection connection = new SqlConnection(SqlConnectionStringBuilder.ConnectionString))
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
                    //new ErrorWindow(exception.Message).ShowDialog(); todo
                }
            }

            return dbNames;
        }

        public void ExecuteQueryToInstance(string query)
        {
            using (SqlConnection connection = new SqlConnection(SqlConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Connection.Open();

                    command.ExecuteNonQuery();

                    command.Connection.Close();
                }
                catch (Exception exception)
                {
                    //new ErrorWindow(exception.Message).ShowDialog(); todo
                }
            }
        }
    }
}
