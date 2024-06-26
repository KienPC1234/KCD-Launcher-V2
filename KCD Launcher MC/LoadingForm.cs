using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO.Compression;
using KCD_Launcher_MC.AppCode;
using System.Windows.Forms;
using KCD_Lib;
using System.IO;
using KCD_Launcher_MC.AppCode.Data;
using Aspose.Html;
using System.Net;
using Aspose.Html.Net;

namespace KCD_Launcher_MC
{
    public partial class LoadingForm : Form
    {
        private AppInfo info = new AppInfo();
        private SupplyTool SupplyTool = new SupplyTool();
        private Misc misc = new Misc();
        public LoadingForm()
        {
            InitializeComponent();
            InitializeAsync();
            if (!Directory.Exists(Path.Combine(info.AppBase,"Logs")))
            {
                Directory.CreateDirectory(Path.Combine(info.AppBase, "Logs"));
            }
            if (!Directory.Exists(Path.Combine(info.AppBase, "Data")))
            {
                Directory.CreateDirectory(Path.Combine(info.AppBase, "Data"));
            }
            if (!Directory.Exists(info.Temp))
            {
                Directory.CreateDirectory(info.Temp);
            }
            if (!Directory.Exists(Path.Combine(info.AppBase,"Tools")))
            {
                Directory.CreateDirectory(Path.Combine(info.AppBase, "Tools"));
            }
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Directory.Delete(info.Temp, true);
        }

        void Check7z()
        {
            string sevenzex = Path.Combine(info.AppBase, "Tools", "7z");
            if (!Directory.Exists(sevenzex)||!File.Exists(sevenzex+ "\\7za.exe"))
            {
                SupplyTool.LogController("Can't Find 7z!", "WARN", Path.Combine(info.AppBase, "Logs"), info.LogFileName);
                MessageBox.Show("Phần Mềm Đang Bị Thiếu File Quan Trọng Cần Tải!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kryptonProgressBar1.Text = "Downloading File...";
                if (!SupplyTool.IsInternetAvailable())
                {
                    DialogResult rs = MessageBox.Show("Không Thế Kết Nối Internet!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (rs == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
                Directory.CreateDirectory(sevenzex);
                try
                {
                    var document = new HTMLDocument();
                    var url = new Url(misc.GetKCD_Info("sevenzip"));
                    var request = new RequestMessage(url);
                    var response = document.Context.Network.Send(request);

                    if (response.IsSuccess)
                    {
                        File.WriteAllBytes(Path.Combine(info.Temp, url.Pathname.Split('/').Last()), response.Content.ReadAsByteArray());
                        ZipFile.ExtractToDirectory(info.Temp+ "\\7z.zip", Path.Combine(info.AppBase, "Tools"));
                    }

                }
                catch (Exception ex)
                {

                    misc.Error(ex.Message);
                }
            }
        }
        void CheckNodeJS()
        {
            string Nodejsex = Path.Combine(info.AppBase, "Tools", "NodeJSPortable");
            if (!Directory.Exists(Nodejsex))
            {
                SupplyTool.LogController("Can't Find NodeJS!", "WARN", Path.Combine(info.AppBase, "Logs"), info.LogFileName);
                MessageBox.Show("Phần Mềm Đang Bị Thiếu File Quan Trọng Cần Tải!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kryptonProgressBar1.Text = "Downloading File...";
                if (!SupplyTool.IsInternetAvailable()) {
                    DialogResult rs = MessageBox.Show("Không Thế Kết Nối Internet!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (rs == DialogResult.OK) { 
                        Application.Exit();
                    }
                }
                Directory.CreateDirectory(Nodejsex);
                try
                {
                    var document = new HTMLDocument();
                    var url = new Url(misc.GetKCD_Info("nodejsurl"));
                    var request = new RequestMessage(url);
                    var response = document.Context.Network.Send(request);

                    if (response.IsSuccess)
                    {
                        File.WriteAllBytes(Path.Combine(info.Temp, url.Pathname.Split('/').Last()), response.Content.ReadAsByteArray());
                        SupplyTool.LogController(info.Temp + "\\NodeJSPortable_6.14.2.zip"+ Nodejsex,"INFO", Path.Combine(info.AppBase, "Logs"),info.LogFileName);
                        misc.unzip(info.Temp+ "\\NodeJSPortable_6.14.2.zip",Nodejsex);
                    }

                }
                catch (Exception ex)
                {

                    misc.Error(ex.Message);
                }
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            (new MainForm()).Show();
        }
        private async void InitializeAsync()
        {
            string thumuclog = Path.Combine(info.AppBase, "Logs");
            try
            {                
                if (!SupplyTool.IsInternetAvailable())
                {
                    SupplyTool.LogController("No Internet!","WARN",thumuclog,info.LogFileName);
                    MessageBox.Show("Không Thể Kiểm Tra Cập Nhập!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    float data = await SupplyTool.FetchAndParseFloatAsync("https://raw.githubusercontent.com/KienPC1234/Vercheck/main/KCDLauncher.txt");
                    if (data > info.Appver)
                    {
                        SupplyTool.LogController("This Application Is Outdated!", "WARN", thumuclog, info.LogFileName);
                        MessageBox.Show("Phần Mềm Hiển Tại Đã Lỗi Thời!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                misc.Error(ex.Message);
            }
            Check7z();
            CheckNodeJS();
            SupplyTool.LogController("App has finished loading!", "INFO", thumuclog, info.LogFileName);
            this.Close();

        }
    }
}
