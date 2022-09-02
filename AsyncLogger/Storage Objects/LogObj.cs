using System;
using AsyncLogger.Enums;

namespace AsyncLogger.StorageObjects
{
    public class LogObj                                                         //Class so store informations for a single log
    {
        public string Timestamp
        {
            get { return timestamp; }
        }
        public string Logtype
        {
            get { return logtype; }
        }
        public string Message
        {
            get { return message; }
        }

        private DateTime time;
        private string timestamp;
        private string logtype;
        private string message;

        public LogObj(LogTypes type, string msg)
        {
            time = DateTime.Now;
            this.timestamp = time.ToString("G");                                //Selected format: 6/15/2008 9:15:07 PM
            this.logtype = type.ToString();
            this.message = msg;
        }
    }
}

