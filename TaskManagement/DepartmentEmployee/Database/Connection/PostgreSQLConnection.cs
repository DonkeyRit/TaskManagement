using DepartmentEmployee.Database.ObjectReader;
using Npgsql;
using System;

namespace DepartmentEmployee.Database.Connection
{
    public class PostgreSQLConnection : Connection
    {
        private NpgsqlConnection connection;

        public override void CloseConnection()
        {
            connection.Close();
        }

        public override void Delete(string query)
        {
            NpgsqlDataReader reader = ExecuteReader(query);
            reader.Close();
        }

        public override void Insert(string query)
        {
            NpgsqlDataReader reader = ExecuteReader(query);
            reader.Close();
        }

        public override void Update(string query)
        {
            NpgsqlDataReader reader = ExecuteReader(query);
            reader.Close();
        }

        public override Connection OpenConnection()
        {
            string parameters = Connection.connParameters.GenerateParams();
            //string parameters = "Server=127.0.0.1;Port=5432;Database=TaskPerformance;UserId=postgres;Password=123456;";
            connection = new NpgsqlConnection(parameters);
            connection.Open();

            return this;
        }

        public override Reader Select(string query)
        {
            return new PostgreSQLReader(ExecuteReader(query));
        }

        private NpgsqlDataReader ExecuteReader(string query)
        {
            NpgsqlCommand command = new NpgsqlCommand(query, this.connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            return reader;
        }
    }
}
