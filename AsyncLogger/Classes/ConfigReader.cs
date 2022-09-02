using System;
using System.Xml;
using AsyncLogger.StorageObjects;
using AsyncLogger.Enums;

namespace AsyncLogger.Classes
{
    public static class ConfigReader
    {
        public static bool GetXmlConfig(ConfigObj config)                       //Read the config file add write the information into the associated objects
        {
            bool result = true;
            try
            {
                using (var reader = XmlReader.Create("config.xml"))
                {
                    string fileTarget = Targets.file.ToString();
                    string dbTarget = Targets.database.ToString();

                    reader.ReadToFollowing("target");
                    do                                                          //Go through the targets in the xml-file
                    {
                        reader.MoveToFirstAttribute();

                        if (reader.Value == Targets.file.ToString())            //Target is a file
                        {
                            foreach (LogfileParameterNames param in             //Go through the parameters of the logfile configuration
                                Enum.GetValues(typeof(LogfileParameterNames)))
                            {
                                reader.ReadToFollowing(param.ToString());
                                config.AddFileConfig(param,                     //Add the logfile configuration to the associated object 
                                    reader.ReadElementContentAsString());
                            }
                        }
                        else if (reader.Value == Targets.database.ToString())   //Target is a database
                        {
                            foreach (DatabaseParameterNames param in            //Go through the parameters of the database configuration
                                Enum.GetValues(typeof(DatabaseParameterNames)))
                            {
                                reader.ReadToFollowing(param.ToString());
                                config.AddDatabaseConfig(param,                 //Add the database configuration to the addociated object
                                    reader.ReadElementContentAsString());
                            }
                        }

                    } while (reader.ReadToFollowing("target"));
                }
            }
            catch(System.IO.FileNotFoundException fex)
            {
                ErrorMessenger.AddFileMessage("ConfigReader.cs", fex.Message);
                result = false;
            }

            return result;
        }
    }
}

