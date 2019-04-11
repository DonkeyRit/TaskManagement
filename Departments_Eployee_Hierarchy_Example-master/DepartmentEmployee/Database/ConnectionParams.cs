using System;
using System.Xml;

namespace DepartmentEmployee.Database
{
    public class ConnectionParams
    {
        public string RDBMS { get; set; }
        private string Server;
        private string Port;
        private string Database;
        private string UserId;
        private string Password;

        private const string pathConfig = "../../../Config.xml";
        private const string postgresqlPattern = "Server={0};Port={1};Database={2};UserId={3};Password={4};";

        public ConnectionParams()
        {
            ReadConfig();
        }

        public string GenerateParams()
        {
            switch (this.RDBMS)
            {
                case "PostgreSQL":
                    return String.Format(postgresqlPattern, this.Server, this.Port, this.Database, this.UserId, this.Password);
                default:
                    throw new Exception("This database does not support now.");
            }
        }

        private void ReadConfig()
        {
            XmlDocument document = new XmlDocument();
            document.Load(pathConfig);

            XmlElement xRoot = document.DocumentElement;
            foreach (XmlNode childnode in xRoot)
            {
                switch (childnode.Name)
                {
                    case "RDBMS":
                        this.RDBMS = childnode.InnerText;
                        break;
                    case "Server":
                        this.Server = childnode.InnerText;
                        break;
                    case "Port":
                        this.Port = childnode.InnerText;
                        break;
                    case "Database":
                        this.Database = childnode.InnerText;
                        break;
                    case "UserId":
                        this.UserId = childnode.InnerText;
                        break;
                    case "Password":
                        this.Password = childnode.InnerText;
                        break;
                    default:
                        Console.WriteLine("Params {0} does not exist." + childnode.Name);
                        break;
                }

            }
        }
    }
}
