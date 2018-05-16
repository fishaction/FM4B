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

        public ImportForm()
        {
            InitializeComponent();
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
            for(int i = 0; i < fileName.Length; i++)
            {
                Array.Resize(ref filePaths, filePaths.Length + 1);
                Array.Resize(ref files, files.Length + 1);
                Array.Resize(ref fileExts, fileExts.Length + 1);
                filePaths[filePaths.Length - 1] = fileName[i];
                files[files.Length - 1] = Path.GetFileNameWithoutExtension(fileName[i]);
                fileExts[files.Length - 1] = Path.GetFileName(fileName[i]);
            }
            //MessageBox.Show(filePaths.Length.ToString());
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            for (int i = 0;i < filePaths.Length;i++)
            {
                listBox1.Items.Add(files[i] + fileExts[i]);
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
            if (previewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        /*未実装
private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
{

TreeNode root = treeView1.Nodes[0];

foreach (TreeNode tn in GetAllChild(e.Node.Nodes))
{
tn.Checked = e.Node.Checked;
}

if (e.Node != root)
{
if (e.Node.Parent.Nodes.Count == 1)
{
e.Node.Parent.Checked = e.Node.Checked;
}
MessageBox.Show("test");
}
}

private List<TreeNode> GetAllChild(TreeNodeCollection Nodes)
{
List<TreeNode> ar = new List<TreeNode>();
foreach (TreeNode node in Nodes)
{
ar.Add(node);
if (node.GetNodeCount(false) > 0)
{
ar.AddRange(GetAllChild(node.Nodes));
}
}
return ar;
}*/
    }
}
