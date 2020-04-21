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

        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = @"C:\Notes\Notes.txt";

            if (File.Exists(filePath))
            {
                //Decrypt
                textBox1.Text = File.ReadAllText(filePath);
                
            }
            else
            {
                File.Create(filePath);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            //Encrypt
            File.WriteAllText(@"C:\Notes\Notes.txt", textBox1.Text);

            Application.Exit();
        } 
    }
}
