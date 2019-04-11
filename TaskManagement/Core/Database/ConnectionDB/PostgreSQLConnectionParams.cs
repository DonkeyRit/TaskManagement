namespace Core.Database.ConnectionDB
{
    public class PostgreSQLConnectionParams
    {        
        public string hostname { get; set; }

        public string port { get; set; }

        public string database { get; set; }

        public string user { get; set; }

        public string password { get; set; }
    }
}
