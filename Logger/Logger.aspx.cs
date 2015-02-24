using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logger.BusinessLogic;

namespace Logger
{
    public partial class Logger : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFileLogger_OnClick(object sender, EventArgs e)
        {
            var message = txtMessage.Text;
            var logger = new FileLogger(@"C:\Test\AFTPPath\log.txt");
            logger.Log(1, message);
        }

        protected void btnHttpLogger_OnClick(object sender, EventArgs e)
        {
            var message = txtMessage.Text;
            var logger = new HTTPLogger(Request.Url.AbsoluteUri);
            logger.Log(1, message);
        }

        protected void btnConsoleLogger_OnClick(object sender, EventArgs e)
        {
            var message = txtMessage.Text;
            var logger = new ConsoleLogger();
            logger.Log(1, message);
        }
    }
}