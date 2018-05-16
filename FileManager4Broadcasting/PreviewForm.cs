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

        int playSecs;

        public PreviewForm()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.settings.autoStart = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripLabel1.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            if (trackBar1.Maximum >(int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition)
            {
                trackBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            }
            else
            {
                trackBar1.Maximum = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                trackBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            playSecs = (int)axWindowsMediaPlayer1.currentMedia.duration;
            trackBar1.Maximum = playSecs;
            if (axWindowsMediaPlayer1.Ctlcontrols.currentItem.imageSourceHeight == 0)
            {
                axWindowsMediaPlayer1.uiMode = "invisible";
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[0].Height = 0;
                Height = 100;
                Width = 300;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            timer1.Enabled = false;
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            timer1.Enabled = true;
        }
    }
}