using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_OOP
{
    internal interface ILogger
    {
        void LogError(string message);
        void LogWarning(string message);
        void LogInfo(string message);
    }

    internal class DatabaseLogger : ILogger
    {
        public void LogError(string message) => Console.WriteLine($"{message} Error has been logged to DB");
        public void LogInfo(string message) => Console.WriteLine($"{message} Info has been logged to DB");
        public void LogWarning(string message) => Console.WriteLine($"{message} Warning has been logged to DB");
    }

    internal class CSVFileLogger : ILogger
    {
        public void LogError(string message) => Console.WriteLine($"{message} Error has been logged to CSV File");
        public void LogInfo(string message) => Console.WriteLine($"{message} Info has been logged to CSV File");
        public void LogWarning(string message) => Console.WriteLine($"{message} Warning has been logged to CSV File");
    }

    internal class ConsoleLogger : ILogger
    {
        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{message}\n");
            Console.ResetColor();
        }

        public void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{message}\n");
            Console.ResetColor();
        }

        public void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{message}\n");
            Console.ResetColor();
        }
    }


}
