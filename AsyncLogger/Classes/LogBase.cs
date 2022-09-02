using System;
using System.IO;
using AsyncLogger.StorageObjects;
using AsyncLogger.Enums;

namespace AsyncLogger.Classes
{
    // ----- Base class for the actual logging process -------------------------
    public abstract class LogBase                                                
    {
        public abstract void Logging(LogObj loggingInformations);
    }

    // ----- Class for logging into a file -------------------------------------
    public class FileLog : LogBase                                              
    {
        private string nameOfApplication;
        private string logPath;
        private string logfileName;
        private string logfileExtension;
        private string fullPath;

        public FileLog(ConfigObj configuration, string applicationname)
        {
            nameOfApplication = applicationname;

            logPath = configuration.FileConfig[LogfileParameterNames.path];     //Get configuration from the configuration object
            logfileName = configuration.FileConfig[LogfileParameterNames.name];
            logfileExtension = configuration.
                FileConfig[LogfileParameterNames.extension];

            CreateFullPath();
            CreateLogFile();
        }

        private void CreateFullPath()                                           //Create full path for osx operating system
        {
            fullPath = logPath;

            if (fullPath != "" && fullPath.EndsWith("/") == false)
            {
                fullPath += "/";
            }

            if(logfileName == "")
            {
                logfileName = "log";
            }
            fullPath += logfileName;

            if(logfileExtension == "")
            {
                logfileExtension = "txt";
            }
            if(logfileExtension.StartsWith(".") == false)
            {
                fullPath += ".";
            }

            fullPath += logfileExtension;
        }

        private void CreateLogFile()                                            //Try to create logfile with the given path if no one exists
        {
            if (File.Exists(fullPath) == false)
            {
                try
                {
                    using (StreamWriter sw = File.CreateText(fullPath))
                    {
                        CreateHeader(sw);
                    }              
                }
                catch (Exception ex)
                {
                    if (ex is DirectoryNotFoundException)
                    {
                        ErrorMessenger.AddFileMessage("LogBase.cs", ex.Message);
                    }
                    else if (ex is IOException)
                    {
                        ErrorMessenger.AddFileMessage("LogBase.cs", ex.Message);
                    }
                    else if (ex is NotSupportedException)
                    {
                        ErrorMessenger.AddFileMessage("LogBase.cs", ex.Message);
                    }
                }
            }
        }

        private void CreateHeader(StreamWriter sw)                              //Create a header text for a new logger file
        {
            sw.WriteLine("Application: " + nameOfApplication + "\n\n");
            sw.WriteLine("{0, -8} {1, -25} {2}", "Type:", "Time:", "Message:");
            sw.Write("\n");  
        }

        public override void Logging(LogObj loggingInformations)
        {
            string fullLog = String.Format("{0, -8} {1, -25} {2}",
                loggingInformations.Logtype, loggingInformations.Timestamp,
                loggingInformations.Message);
            if (File.Exists(fullPath))
            {
                using (StreamWriter sw = File.AppendText(fullPath))
                {
                    sw.WriteLine(fullLog);
                }
            }
            else
            {
                ErrorMessenger.AddFileMessage("Logbase.cs",
                    "Try to log a message, but logfile does not exist");
                ErrorMessenger.AddFileMessage("Logbase.cs",
                    "Auto- try to create new logfile");
                CreateLogFile();
            }
        }
    }

    // ----- Class for logging into a file -------------------------------------
    public class DbLog : LogBase                                                
    {
        public DbLog()
        {
            //Need to implement logic to log into a database
        }

        public override void Logging(LogObj loggingInformations)
        {
            throw new NotImplementedException();
        }
    }
}

