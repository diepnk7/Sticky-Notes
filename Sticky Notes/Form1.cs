using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Sticky_Notes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //move windows without title bar
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            bool flag = m.Msg == 132;
            if (flag)
            {
                m.Result = (IntPtr)2;
            }
        }

        #region Registry that open with window
        static void StartWithOS()
        {
            RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\StickyNotes");
            RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            string keyvalue = "1";
            try
            {
                regkey.SetValue("Index", keyvalue);
                regstart.SetValue("StickyNotes", Application.StartupPath + "\\" + Application.ProductName + ".exe");
                regkey.Close();
            }
            catch (System.Exception ex)
            {
            }
        }
        #endregion

        string filePath = @"C:/Notes/Notes.txt";
        private void Form1_Load(object sender, EventArgs e)
        {
            StartWithOS();
            //show in top right
            this.Location = new Point(Screen.FromPoint(this.Location).WorkingArea.Right - this.Width, 0);
            //don't show in taskbar
            this.ShowInTaskbar = false;

            if (File.Exists(filePath) && File.ReadAllText(filePath) != null)
            {
                textBox1.Text = (File.ReadAllText(filePath)).ToString();
                //move cusor to end of text
                textBox1.SelectionLength = 0;
                textBox1.SelectionStart = textBox1.Text.Length;
            }
            else
            {
                File.Create(filePath);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            Hide();
            WindowState = FormWindowState.Minimized;
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            //Encrypt and Write file
            File.WriteAllText(Path.GetFileName(filePath),Convert.ToString(textBox1.Text));

            Application.Exit();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            File.WriteAllText(filePath, textBox1.Text.ToString());
        }

        
    }
}
