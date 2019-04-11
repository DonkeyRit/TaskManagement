using System;
using Core.Database.ObjectReader;

namespace Core.Database.Connection
{
	public abstract class Connection
	{
		protected readonly ConnectionParams _connectionParams;

		protected Connection(ConnectionParams connectionParams)
		{
			_connectionParams = connectionParams;
		}

		public static Connection CreateConnection()
		{
			ConnectionParams connectionParams = ConnectionParams.GetInstance();

			switch (connectionParams.Provider)
			{
				case "MSSQLProvider":
					return new MSSQLConnection(connectionParams);
				case "Npgsql":
					return new PostgreSQLConnection(connectionParams);
				default:
					throw new Exception("This database does not support now.");
			}
		}

		public abstract Reader Select(string query);

		public abstract void Insert(string query);

		public abstract void Update(string query);

		public abstract void Delete(string query);

		public abstract Connection OpenConnection();

		public abstract void CloseConnection();
	}
}
