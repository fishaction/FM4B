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
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }
        TreeNode rootNode = new TreeNode();
        private void ImportForm_Load(object sender, EventArgs e)
        {
            
            rootNode.Text = "root";
            TreeNode testNode1 = new TreeNode();
            testNode1.Text = "テストノード1";
            TreeNode testNode2 = new TreeNode();
            testNode2.Text = "テストノード2";
            TreeNode childTestNode1 = new TreeNode();
            childTestNode1.Text = "子テストノード1";
            TreeNode childTestNode2 = new TreeNode();
            childTestNode2.Text = "子テストノード2";

            treeView1.Nodes.Add(rootNode);
            rootNode.Nodes.Add(testNode1);
            rootNode.Nodes.Add(testNode2);
            testNode1.Nodes.Add(childTestNode1);
            testNode1.Nodes.Add(childTestNode2);

            testNode2.Nodes.Add(childTestNode1);
            testNode2.Nodes.Add(childTestNode2);

            
        }

        private void NodeCheckToggled(object sender,EventArgs e)
        {
            MessageBox.Show(sender.GetType().ToString());
        }

        private TreeNode[] GetAllChild(TreeNode basetn)
        {

            int count = 0;
            TreeNode[] treeNodes = null;

            foreach (TreeNode tn in basetn.Nodes)
            {
                if(tn.Nodes.Count != 0)
                {
                    
                }
            }
        }

        private void ImportForm_Shown(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in GetAllChild(rootNode))
            {

                MessageBox.Show(tn.Text);
            }
        }
    }
}
