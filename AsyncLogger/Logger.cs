using System;
using System.Threading;
using System.Collections.Generic;
using AsyncLogger.Enums;
using AsyncLogger.Classes;
using AsyncLogger.StorageObjects;

namespace AsyncLogger
{
    public class Logger
    {
        private ConfigObj config;
        private LogBase logger;
        private bool readyToLog = false;

        private Thread loggerThread;
        private Queue<LogObj> logQueue;
        private bool exitThread = false;

        public Logger(string applicationname, Targets target)
        {
            config = new ConfigObj();
            if (ConfigReader.GetXmlConfig(config) == true)
            {
                switch (target)
                {
                    case Targets.file:
                        logger = new FileLog(config, applicationname);
                        readyToLog = true;
                        break;
                    case Targets.database:
                        //Needed to implement logic for database logging
                        break;
                }

                logQueue = new Queue<LogObj>();
                loggerThread = new Thread(new ThreadStart(this.EnterLog));      //Create and start thread for logging the informations into a logfile
                loggerThread.Priority = ThreadPriority.Lowest;
                loggerThread.Start();
            }
            else
            {
                ErrorMessenger.AddFileMessage("Logger.cs",
                    "New instance of logger is needed");
            }

        }

        public void Log(LogTypes type, string message)                          
        {
            logQueue.Enqueue(new LogObj(type, message));
        }

        private void EnterLog()
        {
            while (true)
            {
                if(readyToLog == true && logQueue.Count != 0)
                {
                    LogObj log = logQueue.Dequeue();
                    logger.Logging(log);
                }
                if (exitThread == true)
                {
                    break;
                }

                Thread.Sleep(100);
            }
        }

        public void Stop()                                                      //After calling this method, you cant log any other information
        {
            exitThread = true;
        }
    }
}

