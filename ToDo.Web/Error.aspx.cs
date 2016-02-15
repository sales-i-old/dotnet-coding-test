using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToDo.Web
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            StringBuilder errorMessage = new StringBuilder();

            //Set generic exception if not found
            if (ex == null)
            {
                ex = new Exception("An error has occurred. Please try again or contact our support team if the problem persists.");
            }
            
            //Detailed information - possibly hide this in a production environment
            if (ex.InnerException != null)
            {
                errorMessage.AppendLine(string.Format("<br />{0} <br/><b>Inner exception</b><br />{1}<br /><b>Trace</b><br />{2}", 
                    ex.GetType().ToString(), ex.InnerException.Message, ex.InnerException.StackTrace));

                ltException.Text = errorMessage.ToString();
            }
            else
            {
                errorMessage.AppendLine(string.Format("<br />{0} <br/><b>Type</b><br />{1}", ex.Message, ex.GetType().ToString()));

                if (ex.StackTrace != null)
                {
                    errorMessage.AppendLine(string.Format("<br /><b>Trace</b><br />{0}", ex.StackTrace.ToString().TrimStart()));
                }

                ltException.Text = errorMessage.ToString();
            }

            //TODO: This could be extended to include application (using global.asax) and page errors.  
            //Error handling in seperate project

            Server.ClearError();
        }
    }
}