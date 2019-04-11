using System.Configuration;

namespace Core.Database
{
	public class ConnectionParams
	{
		private static ConnectionParams instance;
		public string Provider { get; }
		public string ConnectionString { get; }

		public static ConnectionParams GetInstance()
		{
			return instance ?? (instance = new ConnectionParams());
		}

		private ConnectionParams()
		{
			var configuration = ConfigurationManager.ConnectionStrings["DefaultConnection"];
			Provider = configuration.ProviderName;
			ConnectionString = configuration.ConnectionString;
		}
	}
}
