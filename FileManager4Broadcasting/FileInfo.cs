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
    public partial class FileInfo : Form
    {

        public FilesAttribute filesAttribute = new FilesAttribute();
        int number;
        ResourceType resourceType;
        public string projectName;

        public FileInfo()
        {
            InitializeComponent();
        }

        private void FileInfo_Shown(object sender, EventArgs e)
        {
            fileName.Text = filesAttribute.FileName;
            filePath.Text = filesAttribute.FilePath;
            if(filesAttribute.Tags != null)
                tagBox.Text = string.Join(",", filesAttribute.Tags);
            memoBox.Text = filesAttribute.Description;
            number = filesAttribute.Number;
            switch (filesAttribute.ResourceType)
            {
                case "Video":
                    resourceType = ResourceType.Video;
                    break;
                case "Image":
                    resourceType = ResourceType.Image;
                    break;
                case "Sound":
                    resourceType = ResourceType.Sound;
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddResource.ModifyJsonFile(number, filePath.Text, projectName, memoBox.Text,resourceType,tagBox.Text.Split(','),filesAttribute.CreatedDate,false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PreviewForm pf = new PreviewForm();
            pf.fileUrl = filesAttribute.FilePath;
            pf.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(
    "EXPLORER.EXE", "/select,"+"\""+filesAttribute.FilePath+"\"");
        }
    }
}
