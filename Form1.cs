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
using Accord.Audio;
using Accord.Math;
using System.IO;


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
           
            byte[] buffer = File.ReadAllBytes(@"d:\zz\zz.wav");
            double logLengthe = Math.Ceiling(Math.Log((double)buffer.Length, 2.0));
            int nPoint = (int)Math.Pow(2.0, Math.Min(Math.Max(1.0, logLengthe), 14.0));
            double[] data2 = new double[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                data2[i] = Math.Sin(buffer[i]);
            }
            double[] fftt = new double[nPoint];
            System.Numerics.Complex[] fftComplex2 = new System.Numerics.Complex[nPoint];
            for (int i = 0; i < nPoint; i++)
            {
                fftComplex2[i] = new System.Numerics.Complex(data2[i], 0.0);
            }
            Accord.Math.FourierTransform.FFT(fftComplex2, Accord.Math.FourierTransform.Direction.Forward);

            for (int i = 0; i < nPoint; i++)
            {
                fftt[i] =  fftComplex2[i].Magnitude;
            }
            double averageFFt = 0.0;
            for (int j = 0; j < nPoint; j++)
            {


                averageFFt += fftt[j];
                
            }
          //  averageFFt = averageFFt / nPoint;
            
            double x = Math.Abs(averageFFt);
            MessageBox.Show(x.ToString());

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
