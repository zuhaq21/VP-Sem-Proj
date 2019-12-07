using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Project
{
    public partial class Form1 : Form
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern long mciSendString(string command, StringBuilder retstring, int returnLength, IntPtr callBack);
        public Form1()
        {
            InitializeComponent();
            mciSendString("open new Type waveaudio alias recsound", null, 0, IntPtr.Zero);
            speakButton.Click += new EventHandler(this.speakButton_Click);

        }
        private void buttonclick(object sender, EventHandler e)
        {
            //throw new NotImplementedException();
            mciSendString("record recsound", null, 0, IntPtr.Zero);
            checkButton.Click += new EventHandler(this.checkButton_Click);
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            speakButton.Enabled = true;
            checkButton.Enabled = false;
          //  throw new NotImplementedException();
            mciSendString("save recsound d:\\zz\\zz.wav", null, 0, IntPtr.Zero);
            mciSendString("close recsound", null, 0, IntPtr.Zero);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void speakButton_Click(object sender, EventArgs e)
        {
            speakButton.Enabled = false;
            checkButton.Enabled = true;
            mciSendString("record recsound", null, 0, IntPtr.Zero);
            checkButton.Click += new EventHandler(this.checkButton_Click);
        }
    }
}
