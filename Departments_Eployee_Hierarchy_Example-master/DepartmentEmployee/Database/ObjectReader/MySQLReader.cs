using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DepartmentEmployee.Database.ObjectReader
{
    public class MySQLReader : Reader
    {
        private MySqlDataReader reader;

        public MySQLReader(MySqlDataReader reader)
        {
            this.reader = reader;
        }

        public override void Close()
        {
            this.reader.Close();
        }

        public override List<object> GetValue(int index, bool type)
        {
            List<object> list = new List<object>();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    if (type)
                    {
                        list.Add(reader.GetString(index));
                    }
                    else
                    {
                        list.Add(reader.GetInt16(index));
                    }
                }
            }
            else
            {
                return null;
            }

            return list;
        }

        public override List<string[]> GetValues()
        {
            List<string[]> values = new List<string[]>();

            while (this.reader.Read())
            {
                int n = reader.FieldCount;
                string[] temp = new string[n];

                for (int i = 0; i < n; i++)
                {
                    temp[i] = reader[i].ToString();
                }
                values.Add(temp);
            }

            return values;
        }
    }
}
