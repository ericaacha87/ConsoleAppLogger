using System;
using ConsoleApp.Business;
using ConsoleApp.Common;
using ConsoleApp.DTOs;

namespace ConsoleApp
{
    class Program
    {
        private static Logger _logger;

        static void Main(string[] args)
        {
            _logger = new GetLogger().GetLoggerObject();

            MessageDTO messageDto = new MessageDTO() { Text = "Log message text", IsMessage = false, IsWarning = true, IsError = false };
            Response response = _logger.LogMessage(messageDto);

            if (response.IsSuccess == false)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(response.Text);
            }
            else
                Console.WriteLine("Log finished successfully. Press a key to exit.");

            Console.ReadKey();
        }
    }

    
    
}
