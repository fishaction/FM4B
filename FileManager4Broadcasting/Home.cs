﻿using System;
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
using Newtonsoft.Json.Linq;

namespace FileManager4Broadcasting
{
    public partial class Home : Form
    {

        public string[] filePaths;
        private string[] fileNames = { @"\プロジェクト", @"\素材" };

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
            SettingSaveLocation();
        }
        
        private void SettingSaveLocation()
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
                    saveLocation += @"\FM4B";
                    foreach (string name in fileNames)
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

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectForm npf = new NewProjectForm();
            if (npf.ShowDialog() == DialogResult.OK)
            {
                CreateProject(npf.textBox1.Text, npf.textBox2.Text, npf.dateTimePicker1.Value);
            }
        }

        public void CreateProject(string name, string description, DateTime dateTime)
        {
            string saveLocation = Properties.Settings.Default.saveLocation;


            if (File.Exists(saveLocation + @"\FM4B\プロジェクト\projects.json"))
            {
                StreamReader sr = new StreamReader(saveLocation + @"\FM4B\プロジェクト\projects.json");
                string json = sr.ReadToEnd();
                sr.Close();
                Project project = JsonConvert.DeserializeObject<Project>(json);

                //汚いコードここから
                DataSet set = new DataSet();
                DataTable table = new DataTable("Projects");
                DataColumn number = new DataColumn("Number", typeof(int));
                number.AutoIncrement = true;
                DataColumn projectName = new DataColumn("ProjectName", typeof(string));
                DataColumn descri = new DataColumn("Description", typeof(string));
                DataColumn date = new DataColumn("Date", typeof(DateTime));
                table.Columns.Add(number);
                table.Columns.Add(projectName);
                table.Columns.Add(descri);
                table.Columns.Add(date);
                set.Tables.Add(table);
                //ここまで

                if (project == null)
                {
                    DataRow r = table.NewRow();
                    r["ProjectName"] = name;
                    r["Description"] = description;
                    r["Date"] = dateTime;
                    table.Rows.Add(r);
                    json = JsonConvert.SerializeObject(set, Formatting.Indented);
                    StreamWriter s = new StreamWriter(saveLocation + @"\FM4B\プロジェクト\projects.json",
                    false);
                    s.Write(json);
                    s.Close();

                    return;
                }

                foreach (ProjectInfo p in project.Projects)
                {
                    DataRow r = table.NewRow();
                    r["ProjectName"] = p.ProjectName;
                    r["Description"] = p.Description;
                    r["Date"] = p.Date;
                    table.Rows.Add(r);
                }
                DataRow row = table.NewRow();
                row["ProjectName"] = name;
                row["Description"] = description;
                row["Date"] = dateTime;
                table.Rows.Add(row);
                json = JsonConvert.SerializeObject(set, Formatting.Indented);
                StreamWriter sw = new StreamWriter(saveLocation + @"\FM4B\プロジェクト\projects.json",
                false);
                sw.Write(json);
                sw.Close();
            }
            else
            {
                StreamWriter sw = new StreamWriter(saveLocation + @"\FM4B\プロジェクト\projects.json",
                false);
                //汚いコードここから
                DataSet set = new DataSet();
                DataTable table = new DataTable("Projects");
                DataColumn number = new DataColumn("Number", typeof(int));
                number.AutoIncrement = true;
                DataColumn projectName = new DataColumn("ProjectName", typeof(string));
                DataColumn descri = new DataColumn("Description", typeof(string));
                DataColumn date = new DataColumn("Date", typeof(DateTime));
                table.Columns.Add(number);
                table.Columns.Add(projectName);
                table.Columns.Add(descri);
                table.Columns.Add(date);
                set.Tables.Add(table);
                //ここまで
                DataRow row = table.NewRow();
                row["ProjectName"] = name;
                row["Description"] = description;
                row["Date"] = dateTime;
                table.Rows.Add(row);
                string json = JsonConvert.SerializeObject(set, Formatting.Indented);
                sw.Write(json);
                sw.Close();
            }
        }

        private void Home_Shown(object sender, EventArgs e)
        {
            if (!Directory.Exists(Properties.Settings.Default.saveLocation))
            {
                MessageBox.Show("ファイルの保存先を選択してください。");
                SettingSaveLocation();
            }
        }
    }

    class Project
    {
        public List<ProjectInfo> Projects { get; set; }
    }

    class ProjectInfo
    {
        public int Number { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }

}
