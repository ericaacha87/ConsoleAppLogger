using System.Data.SqlClient;
using ConsoleApp.Models;
using System.Configuration;
using ConsoleApp.Common;
using System;

namespace ConsoleApp.Infrastructure
{
    public class LogToDatabase
    {
        private string _connectionString;
        private SqlConnection _sqlConnection;

     


        public Response Log(Message message)
        {
            Response response = new Response();
            try
            {
                _connectionString = ConfigurationManager.AppSettings["LogAplication"];
                _sqlConnection = new SqlConnection(_connectionString);
                string insertStatement = "insert into Log_Values VALUES('" + message.Text + "', " + message.Type.Id + ")";
                SqlCommand sqlCommand = new SqlCommand(insertStatement, _sqlConnection);
                _sqlConnection.Open();
                if (sqlCommand.ExecuteNonQuery() > 0)
                    response.IsSuccess = true;
                else
                {
                    response.IsSuccess = false;
                    response.Text = "Error: Log Database.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Text ="Error Log Database:"+ e.Message;
            }

            return response;
        }
        
                  
              
}
}
