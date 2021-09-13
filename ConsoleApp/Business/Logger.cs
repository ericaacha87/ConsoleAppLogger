using System;
using ConsoleApp.Infrastructure;
using ConsoleApp.Models;
using ConsoleApp.DTOs;
using ConsoleApp.Common;

namespace ConsoleApp.Business
{
   public class Logger
    {
        private static bool _IsAbleLogToFile;
        private static bool _IsAbleLogToConsole;
        private static bool logMessage;
        private static bool logWarning;
        private static bool logError;
        private static bool _IsAbleLogToDatabase;

      

        public Logger(bool logToFileParam, bool logToConsoleParam, bool logToDatabaseParam, bool logMessageParam, bool logWarningParam, bool logErrorParam) //, IDictionary dbParamsMap)
        {
            logError = logErrorParam;
            logMessage = logMessageParam;
            logWarning = logWarningParam;
            _IsAbleLogToDatabase = logToDatabaseParam;
            _IsAbleLogToFile = logToFileParam;
            _IsAbleLogToConsole = logToConsoleParam;
          
        }

        public bool IsValidMessageText(string messageText)
        {
            return (!(messageText == null || messageText.Length == 0));
        }

        public bool IsValidMessageType(bool message, bool warning, bool error)
        {
            int countType = 0;
            if (message)
                countType++;
            if (warning)
                countType++;
            if (error)
                countType++;

            return (countType==1);
        }

        public bool IsValidConfigurationOutput()
        {
            return (_IsAbleLogToFile == false && _IsAbleLogToConsole == false && _IsAbleLogToDatabase == false);
        }

        public bool IsValidConfigurationTypeOutput()
        {
            return (!logError && !logMessage && !logWarning);
        }

        public bool IsValidToLog(string type)
        {
            if (logMessage && type == "Message")
                return true;

            if (logError && type == "Error")
                return true;

            if (logWarning && type == "Warning")
                return true;

            return false;
        }

        public Response LogMessage(MessageDTO messageDto)
        {
            Response response = new Response();
            try
            {
                if (IsValidConfigurationOutput())
                {
                    response.IsSuccess = false;
                    response.Text = "Error Configuration Output. Must selected File, Console and/or Database.";
                    return response;
                }

                if (IsValidConfigurationTypeOutput())
                {
                    response.IsSuccess = false;
                    response.Text = "Error Configuration type output. Must selected Message, Warning and/or Error.";
                    return response;
                }

                if (!IsValidMessageText(messageDto.Text))
                {
                    response.IsSuccess = false;
                    response.Text = "Error: Text message empty.";
                    return response;
                }

                if (!IsValidMessageType(messageDto.IsMessage, messageDto.IsWarning, messageDto.IsError))
                {
                    response.IsSuccess = false;
                    response.Text = "Error: Type of Message invalid. It must be Warning or Error or Message.";
                    return response;
                }

                Message messageObj = Mapper.MapFromMessageDTO(messageDto);

                if (!IsValidToLog(messageObj.Type.Text))
                {
                    response.IsSuccess = false;
                    response.Text = "Error: type Message no able to log.";
                    return response;
                }
               
                if (_IsAbleLogToFile)
                 {  
                    response = new LogToFile().Log(messageObj);
                    if (!response.IsSuccess)
                    {
                       response.Text = "Error. Execution was interrupted. " + response.Text;
                       return response;
                    }
                   
                 }

                if (_IsAbleLogToConsole)
                {
                     LogToConsole.Log(messageObj);
                }

                if (_IsAbleLogToDatabase)
                {
                    response = new LogToDatabase().Log(messageObj);
                    if (!response.IsSuccess)
                    {
                        response.Text = "Error. Execution was interrupted. " + response.Text;
                        return response;
                    }
                  
                }

            }
            catch (Exception e)
            {
                response.Text = e.Message;
                response.IsSuccess = false;         
            }

            return response;
        }
    }
}
