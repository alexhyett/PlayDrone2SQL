
namespace PlayDrone2SQL
{
    using System;
    public class Logger: ILogger
    {
        /// <summary>
        /// Log a to the console.
        /// </summary>
        /// <param name="message"></param>
        public void LogOperation(string message)
        {
            Console.WriteLine(message);
        }
    }
}
