using DepartmentEmployee.Database.ObjectReader;
using MySql.Data.MySqlClient;

namespace DepartmentEmployee.Database.Connection
{
    public class MySQLConnection : Connection
    {
        private MySqlConnection connection;

        public override void CloseConnection()
        {
            connection.Close();
        }

        public override void Delete(string query)
        {
            MySqlDataReader reader = ExecuteQuery(query);
            reader.Close();
        }

        public override void Insert(string query)
        {
            MySqlDataReader reader = ExecuteQuery(query);
            reader.Close();
        }

        public override void Update(string query)
        {
            MySqlDataReader reader = ExecuteQuery(query);
            reader.Close();
        }

        public override Connection OpenConnection()
        {
            string parameters = Connection.connParameters.GenerateParams();
            connection = new MySqlConnection(parameters);
            connection.Open();

            return this;
        }

        public override Reader Select(string query)
        {
            return new MySQLReader(ExecuteQuery(query));
        }

        private MySqlDataReader ExecuteQuery(string query)
        {
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = query;

            MySqlDataReader reader = command.ExecuteReader();

            return reader;
        }
    }
}
