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
    public partial class Home : Form
    {

        public string[] filePaths;

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
            of.Show();
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
    }
}
