using Npgsql;
using System.Data.Common;

namespace Core.Database.Connection
{
	public class PostgreSqlConnection : Connection
	{
		public PostgreSqlConnection(ConnectionParams connectionParams) : base(new NpgsqlConnection(), connectionParams) {}

		protected override DbDataAdapter CreateAdapter(DbCommand command)
		{
			return new NpgsqlDataAdapter((NpgsqlCommand) command);
		}
	}
}
