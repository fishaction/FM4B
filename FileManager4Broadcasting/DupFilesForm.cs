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
        private int count = 0;
        private int filePathLength = 0;
        public string dupLocation;
        public FilesAttribute[] filesAttributes;
        string projectName;

        public DupFilesForm()
        {
            InitializeComponent();
        }

        private void DupFilesForm_Load(object sender, EventArgs e)
        {
            
        }

        private void DupFilesForm_Shown(object sender, EventArgs e)
        {
            progressBar2.Value = 0;
            progressBar2.Maximum = filesAttributes.Length * 100;
            filePathLength = filesAttributes.Length;
            foreach (FilesAttribute fa in filesAttributes)
            {
                string saveLocation = Properties.Settings.Default.saveLocation + @"\FM4B\プロジェクト\" + projectName;
                count += 1;
                switch (fa.ResourceType)
                {
                    case "Video":
                        saveLocation += @"\映像\"+fa.FileName;
                        break;
                    case "Sound":
                        saveLocation += @"\音声\"+fa.FileName;
                        break;
                    case "Image":
                        saveLocation += @"\画像\"+fa.FileName;
                        break;
                }
                DupFile(fa.FilePath, saveLocation);
                
            }
            Close();
        }

        void DupFile(string filePath,string directoryPath)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            string extension = System.IO.Path.GetExtension(filePath);
            fileNameLabel.Text = fileName + extension;
            progressBar1.Value = 0;
            PInvoke.Win32API copyObject = new PInvoke.Win32API();
            copyObject.ProgressChanged += new PInvoke.Win32API.CopyProgressEventHandler(ProgressChanged);
            PInvoke.Win32API.ResultStatus ret = copyObject.CopyStart(filePath,directoryPath+@"\"+fileName+extension, true);
            switch (ret)
            {
                case PInvoke.Win32API.ResultStatus.Completed:
                    break;
                case PInvoke.Win32API.ResultStatus.Failed:
                    MessageBox.Show("ファイルのコピーに失敗しました。");
                    break;
            }
        }

        void ProgressChanged(object s, PInvoke.Win32API.CopyProgressEventArgs e)
        {
            DoEvents();
            var progresspar = e.TotalFileSize > 0
                ? ((decimal)e.TotalBytesTransferred / (decimal)e.TotalFileSize) * (decimal)100
             : (decimal)0;
            progressBar1.Value = (int)progresspar;
            progressBar2.Value = (int)progresspar + (count-1)*100;
            Text = string.Format("{0}/{1}コピー中-{2}", count, filePathLength, fileNameLabel.Text); 
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