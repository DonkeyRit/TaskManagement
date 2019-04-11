using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using System.Windows.Forms;
using NpgsqlTypes;

namespace DepartmentEmployee.Database.ConnectionDB
{
    public class DB
    {
        private PostgreSQLConnectionParams postgresqlconn_params;
        private NpgsqlConnection conn = new NpgsqlConnection(); //Объявляем переменную нашего Connection

        public DB(PostgreSQLConnectionParams postgresqlconn_params) //В конструкторе при создании экземпляра класса получаем на вход параметры SQL соединеия нашего типа PostgreSQLConnectionParams
        {
            this.postgresqlconn_params = postgresqlconn_params;
        }

        public async Task<bool> ExecSQLAsync(string sqlCommand) //Метод для выполнения произвольной SQL команды
        {
            return await Task.Run(() =>
            {
                    if (conn.State != ConnectionState.Open) //Открываем соединение
                {
                    ConnectionOpen();
                }

                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    if (conn.State != ConnectionState.Closed)
                    {
                        ConnectionClose(); //Закрываем соединение                   
                    }

                    return true;
                }
                catch (Exception ex){ MessageBox.Show("Error executing PostgreSQL Command: " + ex.Message);

                    if (conn.State != ConnectionState.Closed)
                    {
                        ConnectionClose(); //Закрываем соединение                   
                    }

                    return false;
                }
            });
        }

        //Метод для выполнения SQL команды - перегруженный метод для параметризированных запросов. Принимаем коллекцию ключ значение содержащую название параметра (например @name)и значение параметра (например "John Smith")
        public async Task<bool>  ExecSQLAsync(string sqlCommand, Dictionary<string, string> postgresql_parameters_list)
        {
            return await Task.Run(() =>
            {

                if (conn.State != ConnectionState.Open)
            {
                ConnectionOpen();
            }

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, conn);
                cmd.CommandType = CommandType.Text;

                //Перебираем коллекцию параметров и добавляем каждый 
                foreach (KeyValuePair<string, string> postgresql_parameter in postgresql_parameters_list)
                {
                    //аргументы - название параметра, тип данных в поле SQL, длинна строки. Value - соответсвенно значение.
                    cmd.Parameters.Add(new NpgsqlParameter(postgresql_parameter.Key, NpgsqlDbType.Varchar, 255) { Value = postgresql_parameter.Value });
                }

                cmd.ExecuteNonQuery();

                    if (conn.State != ConnectionState.Closed)
                    {
                        ConnectionClose(); //Закрываем соединение                   
                    }

                    return true;

                }
                catch (Exception ex){MessageBox.Show("Error executing PostgreSQL Command: " + ex.Message);

                    if (conn.State != ConnectionState.Closed)
                    {
                        ConnectionClose(); //Закрываем соединение                   
                    }

                    return false;
                }
            });
        }

        //Метод для открытия соединения
        private void ConnectionOpen()
        {
            try
            {
                string postgresqlPattern = "Server=" + postgresqlconn_params.hostname + ";Port=" + postgresqlconn_params.port + ";Database=" + postgresqlconn_params.database + ";UserId=" + postgresqlconn_params.user + "; Password=" + postgresqlconn_params.password + ";";
                //string postgresqlPattern = "Server=127.0.0.1;Port=5432;Database=TaskPerformance;UserId=postgres;Password=123456;";
                conn = new NpgsqlConnection(postgresqlPattern);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();// Открываем соединиение
                }
            }
            catch (Exception ex ){ MessageBox.Show("Error to establish the PostgreSQL Connection: " + ex.Message); }
        }

        private void ConnectionClose() //Метод для зарытия соединения
        {
            try
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close(); //Закрываем соединение                   
                }
            }
            catch (Exception ex) { MessageBox.Show("Error to close PostgreSQL Connection: " + ex.Message); }
        }

        //Метод извлечения DataTable из базы данных PostgreSQL Async
        public async Task<DataTable> GetDatatableFromPostgreSQLAsync(string sqlcommand)
        {
            return await Task.Run(() =>
            {
                if (conn.State != ConnectionState.Open)
                {
                    ConnectionOpen();
                }

                 try
                 {
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlcommand, conn);

                    cmd.CommandType = CommandType.Text;
                    NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);

                    DataTable results = new DataTable();

                    sda.Fill(results);
                       
                //Удаляем первый столбец - там ID шник
                //results.Columns.RemoveAt(0);

                if (conn.State != ConnectionState.Closed)
                {
                    ConnectionClose(); //Закрываем соединение                   
                }

                return results;
                }
                catch
                 {
                   DataTable results = new DataTable();
                   return results;
                 }
            });  
        }

        //Метод извлечения DataTable из базы данных PostgreSQL
        public DataTable GetDatatableFromPostgreSQL(string sqlcommand)
        {
                if (conn.State != ConnectionState.Open)
                {
                    ConnectionOpen();
                }

                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand(sqlcommand, conn);

                    cmd.CommandType = CommandType.Text;
                    NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);

                    DataTable results = new DataTable();

                    sda.Fill(results);

                    //Удаляем первый столбец - там ID шник
                    //results.Columns.RemoveAt(0);

                    if (conn.State != ConnectionState.Closed)
                    {
                        ConnectionClose(); //Закрываем соединение                   
                    }

                    return results;
                }
                catch
                {
                    DataTable results = new DataTable();
                    return results;
                }
        }
    }
}
