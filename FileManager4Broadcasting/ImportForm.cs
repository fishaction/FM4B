using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileManager4Broadcasting
{
    public partial class ImportForm : Form
    {

        private string[] files = new string[0];
        private string[] filePaths = new string[0];
        private string[] fileExts = new string[0];

        private Dictionary<string, string> importFilesDictionary = new Dictionary<string, string>();

        public ImportForm()
        {
            InitializeComponent();
            importButton.Enabled = false;
            inportCancelButton.Enabled = false;
            previewButton.Enabled = false;
        }
        TreeNode rootNode = new TreeNode();
        
        private void ImportForm_Load(object sender, EventArgs e)
        {
            
        }

        private void NodeCheckToggled(object sender,EventArgs e)
        {
            MessageBox.Show(sender.GetType().ToString());
        }

        private void ImportForm_Shown(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName =
                    (string[])e.Data.GetData(DataFormats.FileDrop, false);

            AddItems(fileName);
        }

        private void AddItems(string[] fileNames)
        {
            for (int i = 0; i < fileNames.Length; i++)
            {
                Array.Resize(ref filePaths, filePaths.Length + 1);
                Array.Resize(ref files, files.Length + 1);
                Array.Resize(ref fileExts, fileExts.Length + 1);
                filePaths[filePaths.Length - 1] = fileNames[i];
                files[files.Length - 1] = Path.GetFileNameWithoutExtension(fileNames[i]);
                fileExts[files.Length - 1] = Path.GetFileName(fileNames[i]);
            }
            //MessageBox.Show(filePaths.Length.ToString());
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            for (int i = 0;i < filePaths.Length;i++)
            {
                listBox1.Items.Add(fileExts[i]+"("+filePaths[i]+")");
            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            //コントロール内にドラッグされたとき実行される
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            else
                //ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PreviewForm previewForm = new PreviewForm();
            int n = 0;
            for (int i = 0;i<listBox1.Items.Count;i++)
            {
                if (listBox1.Items[i] == listBox1.SelectedItems[0])
                {
                    n = i;
                }
            }

            previewForm.fileUrl = filePaths[n];
            MessageBox.Show(filePaths[n]);
            //MessageBox.Show(filePaths[n]);
            //previewForm.fileUrl = filePaths[n] ;
            
            if (previewForm.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(listBox1.SelectedIndices.ToString());
            if (listBox1.SelectedItems.Count == 1)
            {
                previewButton.Enabled = true;
            }
            else
                previewButton.Enabled = false;
            if (listBox1.SelectedItems.Count >= 1)
            {
                importButton.Enabled = true;
                inportCancelButton.Enabled = true;
            }
            else
            {
                importButton.Enabled = false;
                inportCancelButton.Enabled = false;
            }
            
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            foreach(string s in listBox1.SelectedItems)
            {
                int n = 0;
                for (int i = 0;i<listBox1.Items.Count;i++)
                {
                    if ((string)listBox1.Items[i] == s)
                    {
                        n = i;
                    }
                }
                importFilesDictionary.Add(filePaths[n],s);
            }
            listBox2.Items.Clear();
            foreach(string s in importFilesDictionary.Values)
            {
                listBox2.Items.Add(s);
            }
        }
    }
}
