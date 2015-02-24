using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.BusinessLogic
{
    public class MyLogger
    {
        public string Message { get; set; }

        public MyLogger()
        {
            Message = String.Empty;
        }

        public virtual void Log(int level, string message)
        {
            string levelStr;
            switch (level)
            {
                case 1:
                    levelStr = "INFO";
                    break;
                case 2:
                    levelStr = "WARNING";
                    break;
                case 3:
                    levelStr = "PLSCHECKFFS";
                    break;
                default:
                    levelStr = "ERROR";
                    break;
            }
            var timeStr = DateTime.UtcNow.ToString("O");
            Message = String.Format("{0}::{1}::{2}", levelStr, timeStr, message);
        }
    }
}
;