using System;
using System.IO;

namespace AsyncLogger.Classes
{
    public static class ErrorMessenger
    {
        public static void AddFileMessage(string location, string msg)
        {
            if(File.Exists("Error_of_Logger.txt") == false)                     //Create file and add a header
            {
                using(StreamWriter headerWriter =
                    File.AppendText("Error_of_Logger.txt"))
                {
                    headerWriter.WriteLine("{0, -23} {1, -30} {2}\n",
                        "Time", "File", "Message");
                }
            }
            string time = DateTime.Now.ToString("G");
            using(StreamWriter sw = File.AppendText("Error_of_Logger.txt"))     //Append new error logs to the existing file
            {
                sw.WriteLine("{0, -23} {1, -30} {2}", time, location, msg);
            }
        }
    }
}

