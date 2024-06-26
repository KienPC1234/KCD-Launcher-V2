using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCD_Launcher_MC.AppCode
{
    internal class AppInfo
    {
        public float Appver = 0.1F;
        public string AppBase = AppDomain.CurrentDomain.BaseDirectory;
        public string LogFileName = "Log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public string Temp = Path.Combine(Path.GetTempPath(),"KCDLauncherTemp");
    }
}
