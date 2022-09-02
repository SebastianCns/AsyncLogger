using System;
using System.Collections.Generic;
using AsyncLogger.Enums;

namespace AsyncLogger.StorageObjects
{
    public class ConfigObj                                                      //Object to store the configurations of config.xml file
    {
        public Dictionary<LogfileParameterNames, string> FileConfig
        {
            get { return fileConfig; }
        }
        public Dictionary<DatabaseParameterNames, string> DatabaseConfig
        {
            get { return databaseConfig; }
        }

        private Dictionary<LogfileParameterNames, string> fileConfig;           //Dictionary of the configs for the file logger
        private Dictionary<DatabaseParameterNames, string> databaseConfig;      //Dictionary of the configs for the database logger

        public ConfigObj()
        {
            fileConfig = new Dictionary<LogfileParameterNames, string>();
            databaseConfig = new Dictionary<DatabaseParameterNames, string>();
        }

        public void AddFileConfig(LogfileParameterNames paramName, string param)
        {
            fileConfig.Add(paramName, param);
        }

        public void AddDatabaseConfig(DatabaseParameterNames paramName,
            string param)
        {
            databaseConfig.Add(paramName, param);
        }
    }
}

