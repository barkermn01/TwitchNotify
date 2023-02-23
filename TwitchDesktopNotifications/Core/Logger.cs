using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDesktopNotifications.Core
{
    public class Logger : SingletonFactory<Logger>, Singleton
    {
        private string _name;
        private StreamWriter sw;
        public Logger()
        {
            _name = DateTime.Now.ToString("dd_mm_yyyy HH_mm")+".log";
        }

        ~Logger()
        {
            if (sw != null)
            {
                sw.Flush();
                sw.Close();
            }
        }

        public StreamWriter Writer { 
            get { 
                if(sw == null)
                {
                    String FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TwitchNotify");
                    sw = new StreamWriter(FilePath+_name);
                }
                return sw; 
            } 
        }
    }
}
