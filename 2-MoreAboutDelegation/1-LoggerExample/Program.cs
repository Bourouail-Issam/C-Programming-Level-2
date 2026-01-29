using System;
using System.Collections.Generic;
using System.IO;


namespace _1_LoggerExample
{
    public class Logger
    {
        public delegate void LogAction(string message);

        private LogAction _LogAction;

        public Logger(LogAction action)
        {
            _LogAction = action;
        }
        public void Log(string message)
        {
            _LogAction(message);
        }
    }

    public class Program
    {
        // Method to log to the screen
        public static void LogToScreen(string message)
        {
            Console.WriteLine(message);
        }

        // Method to log to a file
        public static void LogToFile(string message)
        {
            string fileName = "log.txt";
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(message);
            }
        }

        public static void LogToDatabase(string message)
        {
            //write the code to log the message in the database .
            Console.Write(message);
        }

        static void Main(string[] args)
        {
            Logger screenLogger = new Logger(LogToScreen);
            Logger fileLogger = new Logger(LogToFile);
            Logger DBLogger = new Logger(LogToDatabase);

            screenLogger.Log("This message will be displayed on the screen.");
            fileLogger.Log("This message will be logged to a file.");
            DBLogger.Log("This message will be logged to database.");

            // You can easily switch between logging to the screen or a file by changing the Logger instance.
            Console.ReadLine();
        }
    }
}
