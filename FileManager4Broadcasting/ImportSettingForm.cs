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
        FilesAttribute[] filesAttributes = new FilesAttribute[1];

        public ImportSettingForm()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(ResourceType));
        }

        private void ImportSettingForm_Shown(object sender, EventArgs e)
        {
            Array.Resize(ref filesAttributes, filePaths.Length);
            for(int i = 0; i < filePaths.Length; i++)
            {
                filesAttributes[i] = GetFilesAttribute(filePaths[i]);
            }
            UpdateList();
        }

        void UpdateList()
        {
                listBox1.Items.Clear();
                foreach (FilesAttribute fa in filesAttributes)
                {
                    listBox1.Items.Add(Path.GetFileNameWithoutExtension(fa.FileName));
                }
        }

        FilesAttribute GetFilesAttribute(string filePath)
        {
            FilesAttribute fa = new FilesAttribute();
            fa.FileName = Path.GetFileName(filePath);
            fa.FilePath = filePath;
            fa.CreatedDate = File.GetLastWriteTime(filePath);
            return fa;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 1)
            {
                int i = listBox1.SelectedIndices[0];
                fileNameBox.Text = Path.GetFileNameWithoutExtension(filesAttributes[i].FileName);
                createdDateLabel.Text = filesAttributes[i].CreatedDate.ToShortDateString();
            }
        }

        private void BoxTextChanged(object sender, EventArgs e)
        {
            foreach (int i in listBox1.SelectedIndices)
            {
                filesAttributes[i].FileName = fileNameBox.Text;
                filesAttributes[i].Tags = tagBox.Text.Split(',');
                filesAttributes[i].Description = memoLabel.Text;
                filesAttributes[i].ResourceType = comboBox1.Text;
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
            DateTime[] dateTime = new DateTime[filesAttributes.Length];
            for(int i = 0; i < filesAttributes.Length; i++)
            {
                dateTime[i] = filesAttributes[i].CreatedDate;
            }
            Array.Sort(dateTime);

            for(int i = 0; i < dateTime.Length; i++)
            {
                for (int n = 0; n < filesAttributes.Length; n++)
                {
                    if ()
                    {

                    }
                }
            }

            /*foreach (DateTime dt in dateTime)
            {
                foreach (FilesAttribute fa in filesAttributes)
                {
                    if (fa.CreatedDate == dt)
                    {
                        n++;
                        fa.FileName += "-" + n.ToString();
                    }
                }
            }*/
        }
    }
}
