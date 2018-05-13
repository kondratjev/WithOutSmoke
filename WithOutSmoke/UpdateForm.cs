using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace WithOutSmoke
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            var wClient = new WebClient();
            if (File.Exists("updatelog.rtf")) File.Delete("updatelog.rtf");
            wClient.DownloadFile("http://withoutsmokesrv.at.ua/updatelog.rtf", "updatelog.rtf"); // скачивание log-файла
            File.SetAttributes("updatelog.rtf", FileAttributes.Hidden);
            richTextBox1.LoadFile("updatelog.rtf");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("http://withoutsmokesrv.at.ua/WithOutSmoke.exe");
            Application.Exit();
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists("updatelog.rtf")) File.Delete("updatelog.rtf");
        }
    }
}
