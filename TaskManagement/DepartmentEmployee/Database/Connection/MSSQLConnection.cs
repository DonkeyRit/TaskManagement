using System.Data.SqlClient;
using DepartmentEmployee.Database.ObjectReader;

namespace DepartmentEmployee.Database.Connection
{
	public class MSSQLConnection : Connection
	{
		private SqlConnection connection;
		private string connectionString;

		public MSSQLConnection(string connectionString)
		{
			this.connectionString = connectionString;
		}

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

		public override Connection OpenConnection()
		{
			connection = new SqlConnection(connectionString);
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
