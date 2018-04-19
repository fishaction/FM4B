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
            TreeNode childTestNode3 = new TreeNode();
            childTestNode3.Text = "子テストノード3";
            TreeNode childTestNode4 = new TreeNode();
            childTestNode4.Text = "子テストノード4";

            treeView1.Nodes.Add(rootNode);
            rootNode.Nodes.Add(testNode1);
            rootNode.Nodes.Add(testNode2);
            testNode1.Nodes.Add(childTestNode1);
            testNode1.Nodes.Add(childTestNode2);

            testNode2.Nodes.Add(childTestNode3);
            childTestNode3.Nodes.Add(childTestNode4);

            
        }

        private void NodeCheckToggled(object sender,EventArgs e)
        {
            MessageBox.Show(sender.GetType().ToString());
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
        }

        private void ImportForm_Shown(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in GetAllChild(rootNode.Nodes))
            {

                MessageBox.Show(tn.Text);
            }
        }
    }
}
