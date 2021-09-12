using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DTOs
{
    public class GetTypeAbleMessageDTO
    {
        public bool IsMessageAble { get; set; }
        public bool IsWarningAble { get; set; }
        public bool IsErrorAble { get; set; }
    }
}
