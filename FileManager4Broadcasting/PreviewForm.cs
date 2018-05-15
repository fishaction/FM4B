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

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripLabel1.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            
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
    }
}
