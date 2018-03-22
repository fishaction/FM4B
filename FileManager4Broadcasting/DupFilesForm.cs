using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace FileManager4Broadcasting
{
    public partial class DupFilesForm : Form
    {
        public Home home = new Home();
        private bool _stop = false;

        public DupFilesForm()
        {
            InitializeComponent();
        }

        private void DupFilesForm_Load(object sender, EventArgs e)
        {
            foreach (string s in home.filePaths)
                MessageBox.Show(s);
        }

        private void DupFilesForm_Shown(object sender, EventArgs e)
        {
            DupFile(home.filePaths[0], "");
        }

        void DupFile(string filePath,string directoryPath)
        {
            PInvoke.Win32API copyObject = new PInvoke.Win32API();
            copyObject.ProgressChanged += new PInvoke.Win32API.CopyProgressEventHandler(ProgressChanged);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            string extension = System.IO.Path.GetExtension(filePath);
            PInvoke.Win32API.ResultStatus ret = copyObject.CopyStart(filePath, @"C:\Users\Owner\Desktop\テスト用フォルダ\"+fileName+"_duped"+extension, true);
        }

        void ProgressChanged(object s, PInvoke.Win32API.CopyProgressEventArgs e)
        {
            DoEvents();
            var progresspar = e.TotalFileSize > 0
                ? ((decimal)e.TotalBytesTransferred / (decimal)e.TotalFileSize) * (decimal)1000
             : (decimal)0;
            progressBar1.Value = (int)progresspar;
            /*Invoke(new Action(() =>
            {
                
                
            }));*/
            if (_stop)
            {
                e.CopyProgressResult = PInvoke.Win32API.CopyProgressResult.PROGRESS_STOP;
            }
        }
        
        private void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
              new DispatcherOperationCallback(ExitFrames), frame);
            Dispatcher.PushFrame(frame);
        }

        private object ExitFrames(object f)
        {
            ((DispatcherFrame)f).Continue = false;
            return null;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _stop = true;
            Close();
        }

        private void DupFilesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _stop = true;
        }
    }
}
