using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DTOs
{
    public class MessageDTO
    {
        public string Text { get; set; }
        public bool IsMessage { get; set; }
        public bool IsError { get; set; }
        public bool IsWarning { get; set; }
    }
}
