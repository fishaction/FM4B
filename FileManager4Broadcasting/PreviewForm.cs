using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager4Broadcasting
{
    public partial class PreviewForm : Form
    {
        public PreviewForm()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.settings.autoStart = true;
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value / 1000;
            double d = trackBar1.Value;
            d = d / 1000;
            MessageBox.Show(d.ToString());
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double d = axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000;
            int i = (int)d;
            trackBar1.Value = i;
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //trackBar1.Maximum = (int)axWindowsMediaPlayer1.currentMedia.duration * 1000;
            double d = axWindowsMediaPlayer1.currentMedia.duration * 1000;
            int i = (int)d;
            trackBar1.Maximum = i;
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
            //timer1.Enabled = true;
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value / 1000;
            
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }
    }
}
