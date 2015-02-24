using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Logger.BusinessLogic
{
    public class FileLogger : MyLogger
    {
        private readonly string fullFileName;

        public FileLogger(string fullFileName)
        {
            this.fullFileName = fullFileName;
        }

        public override void Log(int level, string message)
        {
            base.Log(level, message);
            using (StreamWriter file = new StreamWriter(fullFileName, true)) // creates the file if it doesn't exist
            {
                file.WriteLine(Message);
            }
        }
    }
}