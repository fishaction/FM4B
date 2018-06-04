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
    public partial class ImportSettingForm : Form
    {
        public string[] filePaths;
        public string projectName;
        public FilesAttribute[] filesAttributes = new FilesAttribute[0];
        string[] existingFileName = new string[0];

        public ImportSettingForm()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(ResourceType));
        }

        private void ImportSettingForm_Shown(object sender, EventArgs e)
        {
            //Array.Resize(ref filesAttributes, filePaths.Length);
            //for(int i = 0; i < filePaths.Length; i++)
            //{
            //    filesAttributes[i] = GetFilesAttribute(filePaths[i]);
            //}
            //UpdateList();
        }

        void UpdateList()
        {
                listBox1.Items.Clear();
                foreach (FilesAttribute fa in filesAttributes)
                {
                    listBox1.Items.Add(fa.FileName);
                }
        }

        FilesAttribute GetFilesAttribute(string filePath)
        {
            FilesAttribute fa = new FilesAttribute();
            fa.FileName = Path.GetFileNameWithoutExtension(filePath);
            fa.FilePath = filePath;
            fa.CreatedDate = File.GetLastWriteTime(filePath);
            fa.ResourceType = "Video";
            return fa;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 1)
            {
                int i = listBox1.SelectedIndices[0];
                fileNameBox.Text = filesAttributes[i].FileName;
                createdDateLabel.Text = filesAttributes[i].CreatedDate.ToShortDateString();
                if (filesAttributes[i].Tags != null)
                    tagBox.Text = string.Join(",", filesAttributes[i].Tags);
                comboBox1.Text = filesAttributes[i].ResourceType;
                memoLabel.Text = filesAttributes[i].Description;
                button3.Enabled = false;
                button4.Enabled = true;
            }
            else
            {
                button3.Enabled = true;
                button4.Enabled = false;
            }
        }

        private void resourceTypeChanged(object sender, EventArgs e)
        {
            foreach (int i in listBox1.SelectedIndices)
            {
                filesAttributes[i].ResourceType = comboBox1.Text;
            }
        }
        private void memoChanged(object sender, EventArgs e)
        {
            foreach (int i in listBox1.SelectedIndices)
            {
                filesAttributes[i].Description = memoLabel.Text;
            }
        }
        private void tagChanged(object sender, EventArgs e)
        {
            foreach (int i in listBox1.SelectedIndices)
            {
                filesAttributes[i].Tags = tagBox.Text.Split(',');
            }
        }

        private void NameBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (int i in listBox1.SelectedIndices)
                {
                    filesAttributes[i].FileName = fileNameBox.Text;
                }
                UpdateList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime[] dateTimes = new DateTime[listBox1.SelectedIndices.Count];
            int count = 0;
            foreach (int i in listBox1.SelectedIndices)
            {
                dateTimes[count] = filesAttributes[i].CreatedDate;
                count++;
            }
            Array.Sort(dateTimes);
            count = 0;
            foreach (DateTime dt in dateTimes)
            {
                for (int i=0;i < listBox1.SelectedIndices.Count;i++)
                {
                    if (filesAttributes[listBox1.SelectedIndices[i]].CreatedDate == dt)
                    {
                        filesAttributes[listBox1.SelectedIndices[i]].FileName += "-" + (count + 1).ToString();
                    }
                }
                count++;
            }
            UpdateList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<FilesAttribute> fas = AddResource.GetFiles(projectName);
            List<string> sList = new List<string>();
            for (int i = 0; i<filesAttributes.Length; i++)
            {
                for(int i2 = 0; i2 < filesAttributes.Length; i2++)
                {
                    if (i != i2)
                    {
                        if (filesAttributes[i].FileName == filesAttributes[i2].FileName)
                        {
                            MessageBox.Show("ファイル名が同じです。変更してください。");
                            break;
                        }
                    }
                }
            }
            if (fas == null)
            {
                for (int i = 0; i < filesAttributes.Length; i++)
                {
                    filesAttributes[i].FileName += Path.GetExtension(filesAttributes[i].FilePath);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                foreach (FilesAttribute fa in filesAttributes)
                {
                    foreach (FilesAttribute fa2 in fas)
                    {
                        if (fa.FileName == Path.GetFileNameWithoutExtension(fa2.FileName))
                        {
                            sList.Add(fa.FileName);
                        }
                    }
                }
                if (sList.Count > 0)
                    MessageBox.Show(string.Join(",", sList) + " は既に存在するファイル名です。ファイル名を変更してください。");
                else
                {
                    for (int i = 0; i < filesAttributes.Length; i++)
                    {
                        filesAttributes[i].FileName += Path.GetExtension(filesAttributes[i].FilePath);
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void ImportSettingForm_DragEnter(object sender, DragEventArgs e)
        {
            //コントロール内にドラッグされたとき実行される
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            else
                //ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;
        }

        private void ImportSettingForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] fns =
                    (string[])e.Data.GetData(DataFormats.FileDrop, false);
            List<string> fileNamesList = new List<string>();
            foreach (string s in fns)
            {
                if (Directory.Exists(s))
                {
                    string[] files = Directory.GetFiles(s, "*", SearchOption.AllDirectories);
                    foreach (string f in files)
                    {
                        fileNamesList.Add(f);
                    }
                }
                else
                {
                    fileNamesList.Add(s);
                }
            }
            int n = filesAttributes.Length;
            Array.Resize(ref filesAttributes, filesAttributes.Length + fileNamesList.Count);
            int i = 0;
            foreach (string s in fileNamesList)
            {
                filesAttributes[n + i] = GetFilesAttribute(s);
                i++;
            }
            UpdateList();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            PreviewForm pf = new PreviewForm();
            pf.fileUrl = filesAttributes[listBox1.SelectedIndices[0]].FilePath;
            pf.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
