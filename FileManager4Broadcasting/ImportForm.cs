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


        private Dictionary<string, string> itemFilesDictionary = new Dictionary<string, string>();
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
                string fileName = Path.GetFileName(fileNames[i]);
                itemFilesDictionary.Add(fileNames[i], fileName);
                /*Array.Resize(ref filePaths, filePaths.Length + 1);
                Array.Resize(ref files, files.Length + 1);
                Array.Resize(ref fileExts, fileExts.Length + 1);
                filePaths[filePaths.Length - 1] = fileNames[i];
                files[files.Length - 1] = Path.GetFileNameWithoutExtension(fileNames[i]);
                fileExts[files.Length - 1] = Path.GetFileName(fileNames[i]);*/
            }
            //MessageBox.Show(filePaths.Length.ToString());
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            /*for (int i = 0;i < filePaths.Length;i++)
            {
                listBox1.Items.Add(fileExts[i]+"("+filePaths[i]+")");
            }*/
            foreach (string s in itemFilesDictionary.Keys)
            {
                listBox1.Items.Add(itemFilesDictionary[s] + "(" + s + ")");
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
            foreach (string s in itemFilesDictionary.Keys)
            {
                string s2 = itemFilesDictionary[s]+"("+s+")";
                if (s2 == (string)listBox1.SelectedItems[0])
                {
                    previewForm.fileUrl = s;
                }
            }
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
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (string s2 in itemFilesDictionary.Keys)
                {
                    
                    string s3 = itemFilesDictionary[s2] + "(" + s2 + ")";
                    if (s == s3)
                    {
                        importFilesDictionary.Add(s2, itemFilesDictionary[s2]);
                        dic.Add(s2, itemFilesDictionary[s2]);
                    }
                }
                foreach (string keys in dic.Keys)
                {
                    itemFilesDictionary.Remove(keys);
                }
            }
            listBox2.Items.Clear();
            foreach (string s in importFilesDictionary.Keys)
            {
                listBox2.Items.Add(importFilesDictionary[s]+"("+s+")");
            }
            UpdateListBox();
        }
    }
}