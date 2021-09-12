using System;
using System.Configuration;
using ConsoleApp.DTOs;

namespace ConsoleApp.Business
{
    public class GetLogger
    {
        private bool IsAbleToSelectTypeLog()
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings["ableToSelectTypeMessage"]);
        }

        private bool GetAnswerTypeMessageEnabled(string question)
        {
            ConsoleKey logMessageKey;
            do
            {
                Console.Write(question);
                logMessageKey = Console.ReadKey().Key;
            } while (logMessageKey != ConsoleKey.Y && logMessageKey != ConsoleKey.N);

            return (logMessageKey == ConsoleKey.Y ? true : false);
        }

        private GetTypeAbleMessageDTO AskTypesAble()
        {
            GetTypeAbleMessageDTO types = new GetTypeAbleMessageDTO();
            types.IsMessageAble = GetAnswerTypeMessageEnabled("\nLog Messages? [Y/N]: ");
            types.IsWarningAble = GetAnswerTypeMessageEnabled("\nLog Warnings? [Y/N]: ");
            types.IsErrorAble = GetAnswerTypeMessageEnabled("\nLog Errors? [Y/N]: ");
            return types;
        }

        
        public Logger GetLoggerObject()
        {
            bool defaultLogToFile = Convert.ToBoolean(ConfigurationManager.AppSettings["logToFile"]);
            bool defaultLogToConsole = Convert.ToBoolean(ConfigurationManager.AppSettings["logToConsole"]);
            bool defaultLogToDatabase = Convert.ToBoolean(ConfigurationManager.AppSettings["logToDatabase"]);
            Logger log;

            if (IsAbleToSelectTypeLog())
            {
                Console.WriteLine("Selection Type is Able (change App.config). Please choose types able to log:");
                GetTypeAbleMessageDTO types = AskTypesAble();
                Console.WriteLine();
                log = new Logger(defaultLogToFile, defaultLogToConsole, defaultLogToDatabase, types.IsMessageAble, types.IsWarningAble, types.IsErrorAble);
            }
            else //take default configuration
            {
                bool defaultLogMessage = Convert.ToBoolean(ConfigurationManager.AppSettings["logMessage"]);
                bool defaultLogWarning = Convert.ToBoolean(ConfigurationManager.AppSettings["logWarning"]);
                bool defaultLogError = Convert.ToBoolean(ConfigurationManager.AppSettings["logError"]);
                log = new Logger(defaultLogToFile, defaultLogToConsole, defaultLogToDatabase, defaultLogMessage, defaultLogWarning, defaultLogError);
            }

            return log;
        }


    }
}
