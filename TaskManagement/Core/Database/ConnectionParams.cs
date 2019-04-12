using System.Configuration;

namespace Core.Database
{
	public class ConnectionParams
	{
		private static ConnectionParams _instance;
		public string Provider { get; }
		public string ConnectionString { get; }

		public static ConnectionParams GetInstance()
		{
			return _instance ?? (_instance = new ConnectionParams());
		}

		private ConnectionParams()
		{
			var configuration = ConfigurationManager.ConnectionStrings["DefaultConnection"];
			Provider = configuration.ProviderName;
			ConnectionString = configuration.ConnectionString;
		}
	}
}
