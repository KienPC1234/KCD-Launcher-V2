using KCD_Launcher_MC.AppCode;
using System;
using KCD_Lib;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KCD_Launcher_MC
{
    public partial class MainForm : Form
    {
        private AppInfo info = new AppInfo();
        private SupplyTool sp = new SupplyTool();
        public MainForm()
        {
            InitializeComponent();
            kryptonScrollBar1.Value = flowLayoutPanel1.VerticalScroll.Value;
            kryptonScrollBar1.Minimum = flowLayoutPanel1.VerticalScroll.Minimum;
            kryptonScrollBar1.Maximum = flowLayoutPanel1.VerticalScroll.Maximum;

            flowLayoutPanel1.ControlAdded += FlowLayoutPanel1_ControlAdded;
            flowLayoutPanel1.ControlRemoved += FlowLayoutPanel1_ControlRemoved;
            this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
        }

        private void FlowLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            kryptonScrollBar1.Minimum = flowLayoutPanel1.VerticalScroll.Minimum;
        }

        private void FlowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            kryptonScrollBar1.Maximum = flowLayoutPanel1.VerticalScroll.Maximum;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void kryptonScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            flowLayoutPanel1.VerticalScroll.Value = kryptonScrollBar1.Value;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Add(new Button() {Text= "OK", Dock=System.Windows.Forms.DockStyle.Top});
        }
    }
}
