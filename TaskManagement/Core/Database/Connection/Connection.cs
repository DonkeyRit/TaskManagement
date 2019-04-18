using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Core.Database.Connection
{
	public abstract class Connection
	{
		private readonly DbConnection _connection;
		protected readonly ConnectionParams ConnectionParams;

		protected Connection(DbConnection connection, ConnectionParams connectionParams)
		{
			_connection = connection;
			ConnectionParams = connectionParams;
		}

		public static Connection CreateConnection()
		{
			var connectionParams = ConnectionParams.GetInstance();

			switch (connectionParams.Provider)
			{
				case "MSSQLProvider":
					return new MssqlConnection(connectionParams);
				case "Npgsql":
					return new PostgreSqlConnection(connectionParams);
				default:
					throw new Exception("This database does not support now.");
			}
		}

		protected void ConnectionOpen()
		{
			try
			{
				if (_connection.State != ConnectionState.Open)
				{
					_connection.Open();
				}
			}
			catch (Exception ex) { MessageBox.Show("Error to establish the Connection: " + ex.Message); }
		}
		protected void ConnectionClose()
		{
			try
			{
				if (_connection.State != ConnectionState.Closed)
				{
					_connection.Close();
				}
			}
			catch (Exception ex) { MessageBox.Show("Error to close Connection: " + ex.Message); }
		}


		public async Task<bool> ExecNonQueryAsync(string sqlCommand)
		{
			return await Task.Run(() => ExecNonQuery(sqlCommand));
		}
		public bool ExecNonQuery(string sqlCommand)
		{
			if (_connection.State != ConnectionState.Open)
			{
				ConnectionOpen();
			}

			try
			{
				var command = _connection.CreateCommand();
				command.CommandText = sqlCommand;
				command.ExecuteNonQuery();

				if (_connection.State != ConnectionState.Closed)
				{
					ConnectionClose();
				}

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error executing PostgreSQL Command: " + ex.Message);

				if (_connection.State != ConnectionState.Closed)
				{
					ConnectionClose();
				}

				return false;
			}
		}

		public DbDataReader GetDataReader(string sqlCommand)
		{
			if (_connection.State != ConnectionState.Open)
			{
				ConnectionOpen();
			}

			try
			{
				var command = _connection.CreateCommand();
				command.CommandText = sqlCommand;

				var reader = command.ExecuteReader();
				return reader;

			}
			catch (Exception ex)
			{
				throw new Exception("Exception", ex);
			}
		}

		public async Task<DataTable> GetDataAdapterAsync(string sqlCommand)
		{
			return await Task.Run(() => GetDataAdapter(sqlCommand));
		}
		public DataTable GetDataAdapter(string sqlCommand)
		{
			if (_connection.State != ConnectionState.Open)
			{
				ConnectionOpen();
			}

			try
			{
				var results = new DataTable();
				var command = _connection.CreateCommand();
				command.CommandText = sqlCommand;

				var adapter = CreateAdapter(command);

				adapter?.Fill(results);

				if (_connection.State != ConnectionState.Closed)
				{
					ConnectionClose();
				}

				return results;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				var results = new DataTable();
				return results;
			}
		}

		protected abstract DbDataAdapter CreateAdapter(DbCommand command);
	}
}
