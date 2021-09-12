using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Models;
using ConsoleApp.DTOs;

namespace ConsoleApp.Common
{
    public class Mapper
    {
        public static Message MapFromMessageDTO(MessageDTO messageDto)
        {
            Message msg = new Message();
            TypeMessage type = new TypeMessage();
            if (messageDto.IsMessage)
            {
                type.Text = "Message";
                type.Id = 1;
            }
            if (messageDto.IsWarning)
            {
                type.Text = "Warning";
                type.Id = 2;
            }
            if (messageDto.IsError)
            {
                type.Text = "Error";
                type.Id = 3;
            }

            msg.Type = type;
            msg.Text = messageDto.Text;

            return msg;
        }
    }
}
