
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ControlPanel
{

    public static class DbProvider
    {
        private static readonly string _connectionString;

        static DbProvider()
        {
            try
            {
                _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            }
            catch (Exception ex)
            {
                Environment.Exit(0);
            }
        }

        public static SqlDataReader GetDataReader(string command)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(command, connection);
                    SqlDataReader result = cmd.ExecuteReader();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static long GetOneParamValueLong(string command)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(command, connection);
                    SqlDataReader result = cmd.ExecuteReader();
                    result.Read();
                    return long.Parse(result[0].ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

         public static long GetOneParamValueLong(SqlCommand command)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    SqlDataReader result = command.ExecuteReader();
                    result.Read();

                    if (result.HasRows)
                    {
                        return long.Parse(result[0].ToString());
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        public static string GetOneParamValueString(SqlCommand command)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    SqlDataReader result = command.ExecuteReader();
                    result.Read();

                    if (result.HasRows)
                    {
                        return result[0].ToString();
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static DataTable GetTable(string command)
        {
            var result = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(command, connection);
                    adapter.Fill(result);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        public static DataSet GetDataSet(SqlCommand command)
        {
            var result = new DataSet();

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    command.Connection = connection;
                    adapter.SelectCommand = command;
                    adapter.Fill(result);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static DataTable GetDataTable(SqlCommand command)
        {
            var result = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    command.Connection = connection;
                    adapter.SelectCommand = command;
                    adapter.Fill(result);
                    return result;
                     
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static long ExecuteCommand(string command)
        {
            long rowsCountaffected = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(command, connection);
                    rowsCountaffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }

            return rowsCountaffected;
        }

        public static long ExecuteCommand(SqlCommand command)
        {
            long rowsCountaffected = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    rowsCountaffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }

            return rowsCountaffected;
        }



    }
}