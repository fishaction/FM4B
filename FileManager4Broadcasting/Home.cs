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
using Newtonsoft.Json;

namespace FileManager4Broadcasting
{
    public partial class Home : Form
    {

        public string[] filePaths;
        private string[] fileNames = {@"\プロジェクト",@"\素材"};

        public Home()
        {
            InitializeComponent();
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void optionItem_Click(object sender, EventArgs e)
        {
            OptionForm of = new OptionForm();
            of.textBox1.Text = Properties.Settings.Default.saveLocation;
            if (of.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.saveLocation = of.textBox1.Text;
                string saveLocation = Properties.Settings.Default.saveLocation;
                if (Directory.Exists(saveLocation))
                {
                    if (!Directory.Exists(saveLocation + @"\FM4B"))
                        Directory.CreateDirectory(saveLocation + @"\FM4B");
                    saveLocation +=@"\FM4B";
                    foreach(string name in fileNames)
                    {
                        if (Directory.Exists(saveLocation + name))
                        {
                            
                        }
                        else
                        {
                            Directory.CreateDirectory(saveLocation + name);
                        }
                    }
                    
                }
                else
                {
                    Properties.Settings.Default.saveLocation = "";
                }
            }
        }



        private void Home_DragDrop(object sender, DragEventArgs e)
        {
            /*開発中*/
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string f in filePaths)
            {
                MessageBox.Show(f);
            }

            DupFilesForm dff = new DupFilesForm();
            dff.home = this;
            dff.Show();
        }

        private void Home_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImportForm importForm = new ImportForm();
            if (importForm.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PreviewForm pf = new PreviewForm();
            pf.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ImportSettingForm isf = new ImportSettingForm();
            isf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
