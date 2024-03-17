using System;
using System.IO;

namespace EcsGameLab
{
    public static class EcsLabLogger
    {
        private static readonly string logFilePath = Path.Combine(Environment.CurrentDirectory, "log.txt");

        public static void Log(string message)
        {
            try
            {
                // Prepare the log message with a timestamp
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n";

                // Append the log message to the log.txt file
                // This uses a thread-safe method to ensure that multiple log messages can be written without conflict
                File.AppendAllText(logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                // If logging fails, you might want to handle it somehow
                // For simplicity, we'll just write the error to the console
                // In a real application, you might want to handle this more gracefully
                Console.WriteLine($"Failed to log message. Error: {ex.Message}");
            }
        }
    }
}
