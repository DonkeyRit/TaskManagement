using System.Data.SqlClient;
using Core.Database.ObjectReader;

namespace Core.Database.Connection
{
	public class MSSQLConnection : Core.Database.Connection.Connection
	{
		private SqlConnection connection;

		public MSSQLConnection(ConnectionParams connectionParams) : base(connectionParams) {}

		public override void CloseConnection()
		{
			connection.Close();
		}

		public override void Delete(string query)
		{
			SqlDataReader reader = ExecuteQuery(query);
			reader.Close();
		}

		public override void Insert(string query)
		{
			SqlDataReader reader = ExecuteQuery(query);
			reader.Close();
		}

		public override void Update(string query)
		{
			SqlDataReader reader = ExecuteQuery(query);
			reader.Close();
		}

		public override Core.Database.Connection.Connection OpenConnection()
		{
			connection = new SqlConnection(_connectionParams.ConnectionString);
			connection.Open();

			return this;
		}

		public override Reader Select(string query)
		{
			return new MSSQLReader(ExecuteQuery(query));
		}

		private SqlDataReader ExecuteQuery(string query)
		{
			SqlCommand command = this.connection.CreateCommand();
			command.CommandText = query;

			SqlDataReader reader = command.ExecuteReader();

			return reader;
		}
	}
}
