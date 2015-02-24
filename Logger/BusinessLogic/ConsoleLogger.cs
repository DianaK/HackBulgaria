using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logger.BusinessLogic
{
    public class ConsoleLogger : MyLogger
    {
        public override void Log(int level, string message)
        {
            base.Log(level, message);
            Console.WriteLine(Message);
            System.Diagnostics.Debug.WriteLine(Message); // logs to output window in visual studio
        }
    }
}