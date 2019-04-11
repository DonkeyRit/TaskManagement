using Npgsql;
using Core.Database;
using DepartmentEmployee.Database.ObjectReader;

namespace DepartmentEmployee.Database.Connection
{
	public class PostgreSQLConnection : Core.Database.Connection.Connection
	{
		private NpgsqlConnection connection;

		public PostgreSQLConnection(ConnectionParams connectionParams) : base(connectionParams) {}

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

		public override Core.Database.Connection.Connection OpenConnection()
		{
			connection = new NpgsqlConnection(_connectionParams.ConnectionString);
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
