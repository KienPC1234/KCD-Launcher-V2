using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KCD_Lib
{
    public class SupplyTool
    {
        public bool IsInternetAvailable()
        {
            try
            {
                using (var ping = new System.Net.NetworkInformation.Ping())
                {
                    var result = ping.Send("google.com");
                    return result.Status == System.Net.NetworkInformation.IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<float> FetchAndParseFloatAsync(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string content = await client.GetStringAsync(url);
                    if (float.TryParse(content, out float result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception($"Unable to parse the content to float {content}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }
        public void LogController(string NoiDung, string KieuLog, string ThuMucLogs, string TenFileLog)
        {
            string fullPath = Path.Combine(ThuMucLogs, TenFileLog + ".log");
            if (!Directory.Exists(ThuMucLogs))
            {
                Directory.CreateDirectory(ThuMucLogs);
            }

            if (!File.Exists(fullPath))
            {
                using (StreamWriter sw = File.CreateText(fullPath))
                {
                    sw.WriteLine($"{DateTime.Now:HH:mm:ss} [{KieuLog}] : {NoiDung}");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(fullPath))
                {
                    sw.WriteLine($"{DateTime.Now:HH:mm:ss} [{KieuLog}] : {NoiDung}");
                }
            }
        }
        
    }
}
