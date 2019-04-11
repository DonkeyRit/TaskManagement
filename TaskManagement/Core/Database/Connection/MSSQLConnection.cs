using System.Data.Common;
using System.Data.SqlClient;

namespace Core.Database.Connection
{
	public class MssqlConnection : Connection
	{
		public MssqlConnection(ConnectionParams connectionParams) : base(new SqlConnection(), connectionParams) {}
		protected override DbDataAdapter CreateAdapter()
		{
			return new SqlDataAdapter();
		}	
	}
}
