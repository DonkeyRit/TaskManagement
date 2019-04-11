using System.Collections.Generic;
using Npgsql;

namespace Core.Database.ObjectReader
{
    public class PostgreSQLReader : Reader
    {
        private NpgsqlDataReader reader;
        private NpgsqlConnection conn = new NpgsqlConnection(); //Объявляем переменную нашего Connection

        public PostgreSQLReader(NpgsqlDataReader reader)
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
                        list.Add(reader.GetInt32(index));
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
