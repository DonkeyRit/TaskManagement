using DepartmentEmployee.Database.ObjectReader;
using System;
using System.Configuration;

namespace DepartmentEmployee.Database.Connection
{
    public abstract class Connection
    {
        protected static Connection connection;
        protected static ConnectionParams connParameters;

        public static Connection CreateConnection(ConnectionParams connParams)
        {
            connParameters = connParams;

            var configuration = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            string provider = configuration.ProviderName,
	            connectionString = configuration.ConnectionString;

            connection = new MSSQLConnection(connectionString);


			/*switch (connParams.RDBMS)
            {
                case "MSSQL":
                    connection = new MSSQLConnection(connectionString);
                    break;
                case "PostgreSQL":
                    connection = new PostgreSQLConnection();
                    break;
                default:
                    throw new Exception("This database does not support now.");
            }*/

            return connection;
        }

        public abstract Reader Select(string query);

        public abstract void Insert(string query);

        public abstract void Update(string query);

        public abstract void Delete(string query);

        public abstract Connection OpenConnection();

        public abstract void CloseConnection();
    }
}
