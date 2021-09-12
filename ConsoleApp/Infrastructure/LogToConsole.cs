using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Infrastucture.Common;

namespace ConsoleApp.Infrastructure
{
    public static class LogToConsole
    {
        private static ConsoleColor GetColorByType(string type)
        {
            ConsoleColor color = ConsoleColor.White;

            if (type == "Message")
            {
                color = ConsoleColor.DarkBlue;
            }

            if (type == "Warning")
            {
                color = ConsoleColor.Yellow;
            }

            if (type == "Error")
            {
                color = ConsoleColor.Red;
            }
            return color;
        }


        public static bool Log(Message msg)
        {
            string textToLog = FormatTextMessage.GetTextToLog(msg);
            Console.BackgroundColor = GetColorByType(msg.Type.Text);
            Console.WriteLine(textToLog);
            Console.ResetColor();
            return true;
        }
    }
}
