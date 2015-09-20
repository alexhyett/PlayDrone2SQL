using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PlayDrone2SQL
{
    public class FileReader
    {
        private Logger log;

        public FileReader(Logger log)
        {
            this.log = log;
        }

        public List<Models.App> FileToModel(string filePath)
        {
            if(string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            if(!File.Exists(filePath))
            {
                throw new ArgumentException(string.Format("File '{0}' does not exist.", filePath));
            }

            log.LogOperation("Starting reading file.");
            var fileContents = File.ReadAllText(filePath);
            log.LogOperation("Finished reading file.");

            log.LogOperation("Starting deserialisation of file.");
            var apps = JsonConvert.DeserializeObject<List<Models.App>>(fileContents);
            log.LogOperation(string.Format("Finished deserialisation of file. {0} apps found.", apps.Count()));

            return apps;
        }
    }
}
