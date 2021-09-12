using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp.Common;
using ConsoleApp.Business;
using ConsoleApp.DTOs;
using System.Configuration;
using System.Reflection;

namespace ConsoleApp.TestConsoleApp
{
    [TestClass]
    public class UnitTest
    {
        [TestInitialize]
        public void Init()
        {
            ConfigurationManager.AppSettings["logFileFolder"] = @"C:\Temp";
            ConfigurationManager.AppSettings["logFile"] = "logFileTest.txt";
            ConfigurationManager.AppSettings["LogAplication"] = "Data Source=localhost;Initial Catalog=LogAplicationTest;Integrated Security=True;Pooling=False";
            ConfigurationManager.AppSettings["ableToSelectTypeMessage"] = "false";

            ConfigurationManager.AppSettings["logToFile"] = "true";
            ConfigurationManager.AppSettings["logToConsole"] = "true";
            ConfigurationManager.AppSettings["logToDatabase"] = "true";

            ConfigurationManager.AppSettings["logMessage"] = "true";
            ConfigurationManager.AppSettings["logWarning"] = "true";
            ConfigurationManager.AppSettings["logError"] = "true";
        }

        [TestMethod]
        public void TestLogErrorToAll_returnOk()
        {
            //arrange
            MessageDTO messageDto = new MessageDTO { Text = "Error register28", IsError = true, IsWarning = false, IsMessage = false };
            
           //act
            Logger logger = new GetLogger().GetLoggerObject();
            Response response = logger.LogMessage(messageDto);

            //assert
            Assert.AreEqual(true, response.IsSuccess);
        }

        [TestMethod]
        public void TestTypeDisable_returnError()
        {
            //arrange
            MessageDTO messageDto = new MessageDTO { Text = "Error register28", IsError = true, IsWarning = false, IsMessage = false }; 
            ConfigurationManager.AppSettings["logError"] = "false";

            //act
            Logger logger = new GetLogger().GetLoggerObject();
            Response response = logger.LogMessage(messageDto);

            //assert
            Assert.AreEqual(false, response.IsSuccess);
            Assert.AreEqual("Error: type Message no able to log.", response.Text);
        }

        [TestMethod]
        public void TestMessageWithoutType_returnError()
        {
            //arrange
            MessageDTO messageDto = new MessageDTO { Text = "Error register28", IsError = false, IsWarning = false, IsMessage = false };

            //act
            Logger logger = new GetLogger().GetLoggerObject();
            Response response = logger.LogMessage(messageDto);

            //assert
            Assert.AreEqual(false, response.IsSuccess);
            Assert.AreEqual("Error: Type of Message invalid. It must be Warning or Error or Message.",response.Text);
        }

        [TestMethod]
        public void TestMessageWithMultipleType_returnError()
        {
            //arrange
            MessageDTO messageDto = new MessageDTO { Text = "mensage", IsError = true, IsWarning = true, IsMessage = false };

            //act
            Logger logger = new GetLogger().GetLoggerObject();
            Response response = logger.LogMessage(messageDto);

            //assert
            Assert.AreEqual(false, response.IsSuccess);
            Assert.AreEqual("Error: Type of Message invalid. It must be Warning or Error or Message.",response.Text);
        }
        [TestMethod]
        public void TestMessageWithoutText_returnError()
        {
            //arrange
            MessageDTO messageDto = new MessageDTO { Text = "", IsError = true, IsWarning = false, IsMessage = false };

            //act
            Logger logger = new GetLogger().GetLoggerObject();
            Response response = logger.LogMessage(messageDto);

            //assert
            Assert.AreEqual(false, response.IsSuccess);
            Assert.AreEqual("Error: Text message empty.",response.Text);
        }


        #region configuration
        [TestMethod]
        public void TestConfigurationWithout_Output_returnError()
        {
            //arrange
            MessageDTO messageDto = new MessageDTO { Text = "mensage", IsError = true, IsWarning = false, IsMessage = false };
            ConfigurationManager.AppSettings["logToFile"] = "false";
            ConfigurationManager.AppSettings["logToConsole"] = "false";
            ConfigurationManager.AppSettings["logToDatabase"] = "false";


            //act
            Logger logger = new GetLogger().GetLoggerObject();
            Response response = logger.LogMessage(messageDto);

            //assert
            Assert.AreEqual(false, response.IsSuccess);
            Assert.AreEqual("Error Configuration Output. Must selected File, Console and/or Database.", response.Text);
        }

        [TestMethod]
        public void TestConfigurationWithout_Type_returnError()
        {
            //arrange
            MessageDTO messageDto = new MessageDTO { Text = "mensage", IsError = true, IsWarning = false, IsMessage = false };
            ConfigurationManager.AppSettings["logToFile"] = "true";
            ConfigurationManager.AppSettings["logToConsole"] = "false";
            ConfigurationManager.AppSettings["logToDatabase"] = "false";

            ConfigurationManager.AppSettings["logMessage"] = "false";
            ConfigurationManager.AppSettings["logWarning"] = "false";
            ConfigurationManager.AppSettings["logError"] = "false";

            //act
            Logger logger = new GetLogger().GetLoggerObject();
            Response response = logger.LogMessage(messageDto);

            //assert
            Assert.AreEqual(false, response.IsSuccess);
            Assert.AreEqual("Error Configuration type output. Must selected Message, Warning and/or Error.", response.Text);
        }


        [TestMethod]
        public void TestConfigurationDatabase_returnError()
        {
            //arrange
            MessageDTO messageDto = new MessageDTO { Text = "mensage", IsError = true, IsWarning = false, IsMessage = false };
            ConfigurationManager.AppSettings["logToFile"] = "false";
            ConfigurationManager.AppSettings["logToConsole"] = "false";
            ConfigurationManager.AppSettings["logToDatabase"] = "true";

            ConfigurationManager.AppSettings["logMessage"] = "false";
            ConfigurationManager.AppSettings["logWarning"] = "true";
            ConfigurationManager.AppSettings["logError"] = "true";

            ConfigurationManager.AppSettings["LogAplication"] = "Data Source=noExiste;Initial Catalog=LogAplicationTest;Integrated Security=True;Pooling=False";
            //act
            Logger logger = new GetLogger().GetLoggerObject();
            Response response = logger.LogMessage(messageDto);

            //assert
            Assert.AreEqual(false, response.IsSuccess);
            Assert.IsTrue(response.Text.Contains("Error. Execution was interrupted. Error Log Database"));
 
        }

        [TestMethod]
        public void TestConfigurationFile_returnError()
        {
            //arrange
            MessageDTO messageDto = new MessageDTO { Text = "mensage", IsError = true, IsWarning = false, IsMessage = false };
            ConfigurationManager.AppSettings["logToFile"] = "true";
            ConfigurationManager.AppSettings["logToConsole"] = "false";
            ConfigurationManager.AppSettings["logToDatabase"] = "false";

            ConfigurationManager.AppSettings["logMessage"] = "false";
            ConfigurationManager.AppSettings["logWarning"] = "true";
            ConfigurationManager.AppSettings["logError"] = "true";

            ConfigurationManager.AppSettings["logFileFolder"] = @"C:";
            //act
            Logger logger = new GetLogger().GetLoggerObject();
            Response response = logger.LogMessage(messageDto);

            //assert
            Assert.AreEqual(false, response.IsSuccess);
            Assert.IsTrue(response.Text.Contains("Error. Execution was interrupted. Error: Log in file"));

        }
        #endregion


    }
}
