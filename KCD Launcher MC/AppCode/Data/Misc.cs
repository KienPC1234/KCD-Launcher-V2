using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json.Linq;
using Aspose.Html;
using KCD_Lib;
using System.Windows.Forms;
using Aspose.Html.Net;
using SevenZip;
using System.Linq;


namespace KCD_Launcher_MC.AppCode.Data
{
    public class Misc
    {
        private AppInfo info = new AppInfo();
        private SupplyTool supplyTool = new SupplyTool();
        public void Error(string error)
        {
            var thumuclog = Path.Combine(info.AppBase, "Logs");
            DialogResult rs = MessageBox.Show("Đã Có Lỗi Xảy Ra, vui lòng gừi file log cho nhà phát triển!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (rs == DialogResult.OK)
            {
                supplyTool.LogController($"An error occurred: {error}", "ERROR", thumuclog, info.LogFileName);
                Process.Start(Path.Combine(info.AppBase, "Logs"), info.LogFileName+ ".log");
                Application.Exit();
            }
        }
        public string GetKCD_Info(string value) 
        {
                string jsonUrl = "https://raw.githubusercontent.com/KienPC1234/Myapp1database/main/KCD_Info.json";
                string jsonContent;
                using (var webClient = new System.Net.WebClient())
                {
                    jsonContent = webClient.DownloadString(jsonUrl);
                }
                JObject jsonObject = JObject.Parse(jsonContent);
                if (jsonObject.TryGetValue(value, out JToken result))
                {
                    return result.ToString();
                }
                else
                {
                    throw new Exception(message:"Can't Take Data!");
                }
        }

        public void unzip(string inFile, string outFile)
        {
            try
            {
                string root = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.WorkingDirectory = @root;
                cmd.Start();

                cmd.StandardInput.WriteLine("cd "+ Path.Combine(info.AppBase, "Tools", "7z"));
                cmd.StandardInput.WriteLine($"7za x -y \"{inFile}\" -o\"{outFile}\"");
                cmd.StandardInput.WriteLine("exit");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
            }
            catch (Exception ex) {
                Error(ex.Message);
            }
        }
        
    }
}
