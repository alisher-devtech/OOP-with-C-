using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FarmingManagementSystem.Utilities
{
    public sealed class DatabaseHelper
    {
        private const string ConnectionString =
            "server=localhost;port=3306;user=root;password=mast86/6RX85h-x_!;database=businessapplication;SslMode=Disabled;";

        private DatabaseHelper() { }

        private static readonly Lazy<DatabaseHelper> _instance =
            new Lazy<DatabaseHelper>(() => new DatabaseHelper());

        public static DatabaseHelper Instance => _instance.Value;

        public MySqlConnection getConnection()
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database connection failed: " + ex.Message);
            }
        }

        public int Update(string query)
        {
            return Update(query, null);
        }

        public int Update(string query, Action<MySqlCommand> configureParameters)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(query, connection))
                {
                    if (configureParameters != null)
                        configureParameters.Invoke(command);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database update failed: " + ex.Message);
            }
        }

        public MySqlDataReader getData(string query)
        {
            return getData(query, null);
        }

        public MySqlDataReader getData(string query, Action<MySqlCommand> configureParameters)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);
                var command = new MySqlCommand(query, connection);

                if (configureParameters != null)
                    configureParameters.Invoke(command);

                connection.Open();

                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database query failed: " + ex.Message);
            }
        }
    }
}