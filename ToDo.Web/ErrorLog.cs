using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ToDo.Web
{
    /// <summary>
    /// Error Logging Class
    /// </summary>
    public class ErrorLog
    {
        private static ErrorLog _instance;
        public static ErrorLog Instance
        {
            get
            {
                return _instance ?? (_instance = new ErrorLog());
            }
        }

        /// <summary>
        /// Write Log
        /// </summary>
        /// <param name="ex">Exception Message</param>
        internal void WriteLog(Exception ex)
        {
            var localPath = new Uri(Path.Combine(GetErrorFolder(), "error_log.log")).LocalPath;
            StreamWriter streamWriter;
            var formattedMessage = new StringBuilder();
            using (streamWriter = new StreamWriter(localPath, true))
            {
                // Get stack trace for the exception with source file information
                var stack = new StackTrace(ex, true);
#if DEBUG
                // Get the top stack frame
                var source = stack.GetFrame(0);
                // Get the line number from the stack frame
                var line = source.GetFileLineNumber();
#endif

                formattedMessage.AppendLine("Date: " + DateTime.Now);
                formattedMessage.AppendLine("Source: " + source.ToString().Replace(Environment.NewLine, string.Empty));
                formattedMessage.AppendLine("Message: " + ex.Message);

                formattedMessage.AppendLine("Line: " + line);
                formattedMessage.AppendLine("------------------------------------------------------------");
                streamWriter.WriteLine(formattedMessage.ToString());
                streamWriter.Flush();
            }

        }

        private static string GetErrorFolder()
        {
            //var dic = ConfigurationManager.GetSection("appSettings") as IDictionary;
            //var folder = (dic == null || dic["ErrorLogFilesDir"] == null) ? "" : dic["ErrorLogFilesDir"].ToString().Trim();
            var folder = ConfigurationManager.AppSettings["ErrorLogFilesDir"];
            var url = Path.Combine(HttpRuntime.AppDomainAppPath, folder);
            return url;
        }
    }
}