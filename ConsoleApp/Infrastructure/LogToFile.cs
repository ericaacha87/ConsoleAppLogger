using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Infrastucture.Common;
using ConsoleApp.Common;

namespace ConsoleApp.Infrastructure
{
    public class LogToFile
    {
        private StreamWriter _file;
        private string _path;

        public LogToFile()
        {
            _path = ConfigurationManager.AppSettings["logFileFolder"] + "\\" + ConfigurationManager.AppSettings["logFile"];
        }



        public Response Log(Message message)
        {
            Response response = new Response();
            try
            {
                if (!File.Exists(_path))
                    _file = File.CreateText(_path);
                else
                    _file = new StreamWriter(_path);

                string textToLog = FormatTextMessage.GetTextToLog(message);
                _file.WriteLine(textToLog);
                _file.Close();
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess=false;
                response.Text = "Error: Log in file. Detail:" + e.Message;
            }

            return response;
        }


    }
}
