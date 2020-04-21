using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            StartWithOS();
            //show in top right
            this.Location = new Point(Screen.FromPoint(this.Location).WorkingArea.Right - this.Width, 0);
            //don't show in taskbar
            this.ShowInTaskbar = false;

            string filePath = @"C:\Notes\Notes.txt";

            if (File.Exists(filePath))
            {
                //Decrypt
                textBox1.Text = File.ReadAllText(filePath);
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
            //Encrypt
            File.WriteAllText(@"C:\Notes\Notes.txt", textBox1.Text);

            Application.Exit();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            File.WriteAllText(@"C:\Notes\Notes.txt", textBox1.Text);
        }
    }
}
