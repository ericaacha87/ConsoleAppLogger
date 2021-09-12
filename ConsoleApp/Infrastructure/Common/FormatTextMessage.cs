using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastucture.Common
{
    public class FormatTextMessage
    {
        public static string GetTextToLog(Message msg)
        {
            return msg.Type.Text + " " + DateTime.Now + " " + msg.Text;
        }
    }
}
