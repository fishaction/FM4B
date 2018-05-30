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
                    MessageBox.Show("正しいパスを選択してください");
                    SettingSaveLocation();
                    Properties.Settings.Default.saveLocation = "";
                }
            }
            else if (of.ShowDialog() == DialogResult.Cancel)
            {
                if (Properties.Settings.Default.saveLocation == "")
                {
                    SettingSaveLocation();
                }
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start:
            NewProjectForm npf = new NewProjectForm();
            if (npf.ShowDialog() == DialogResult.OK)
            {
                bool result = CreateProject(npf.textBox1.Text, npf.textBox2.Text, npf.dateTimePicker1.Value);
                if (!result)
                {
                    MessageBox.Show("既に存在するプロジェクト名、または利用できない文字が使われています。");
                    goto Start;
                }
                ReadProject();
            }
        }

        public bool CreateProject(string name, string description, DateTime dateTime)
        {
            string saveLocation = Properties.Settings.Default.saveLocation;

            System.Text.RegularExpressions.Regex regex =
    new System.Text.RegularExpressions.Regex(
        "[\\x00-\\x1f<>:\"/\\\\|?*]" +
        "|^(CON|PRN|AUX|NUL|COM[0-9]|LPT[0-9]|CLOCK\\$)(\\.|$)" +
        "|[\\. ]$",
    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //マッチしたら、不正なファイル名
            if (regex.IsMatch(name))
            {
                return false;
            }

            if (File.Exists(saveLocation + @"\FM4B\プロジェクト\projects.json"))
            {

                List<ProjectInfo> projects = GetProjectInfos();
                
                //汚いコードここから
                DataSet set = new DataSet();
                DataTable table = new DataTable("Projects");
                DataColumn number = new DataColumn("Number", typeof(int));
                number.AutoIncrement = true;
                DataColumn projectName = new DataColumn("ProjectName", typeof(string));
                DataColumn descri = new DataColumn("Description", typeof(string));
                DataColumn date = new DataColumn("Date", typeof(DateTime));
                DataColumn saveDic = new DataColumn("SaveDirectory", typeof(string));
                table.Columns.Add(number);
                table.Columns.Add(projectName);
                table.Columns.Add(descri);
                table.Columns.Add(date);
                table.Columns.Add(saveDic);
                set.Tables.Add(table);
                //ここまで

                string j = "";

                if (projects == null)
                {
                    DataRow r = table.NewRow();
                    r["ProjectName"] = name;
                    r["Description"] = description;
                    r["Date"] = dateTime;
                    r["SaveDirectory"] = saveLocation + @"\FM4B\プロジェクト";
                    table.Rows.Add(r);
                    j = JsonConvert.SerializeObject(set, Formatting.Indented);
                    StreamWriter s = new StreamWriter(saveLocation + @"\FM4B\プロジェクト\projects.json",
                    false);
                    s.Write(j);
                    s.Close();
                    Directory.CreateDirectory(saveLocation + @"\FM4B\プロジェクト\"+name);
                    return true;
                }

                foreach (ProjectInfo p in projects)
                {

                    if (p.ProjectName == name)
                    {
                        return false;
                    }

                    DataRow r = table.NewRow();
                    r["ProjectName"] = p.ProjectName;
                    r["Description"] = p.Description;
                    r["Date"] = p.Date;
                    r["SaveDirectory"] = saveLocation + @"\FM4B\プロジェクト";
                    table.Rows.Add(r);
                }
                DataRow row = table.NewRow();
                row["ProjectName"] = name;
                row["Description"] = description;
                row["Date"] = dateTime;
                row["SaveDirectory"] = saveLocation + @"\FM4B\プロジェクト";
                table.Rows.Add(row);
                j = JsonConvert.SerializeObject(set, Formatting.Indented);
                StreamWriter sw = new StreamWriter(saveLocation + @"\FM4B\プロジェクト\projects.json",
                false);
                sw.Write(j);
                sw.Close();
                Directory.CreateDirectory(saveLocation + @"\FM4B\プロジェクト\" + name);
                return true;
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
                DataColumn saveDic = new DataColumn("SaveDirectory", typeof(string));
                table.Columns.Add(number);
                table.Columns.Add(projectName);
                table.Columns.Add(descri);
                table.Columns.Add(date);
                table.Columns.Add(saveDic);
                set.Tables.Add(table);
                //ここまで
                DataRow row = table.NewRow();
                row["ProjectName"] = name;
                row["Description"] = description;
                row["Date"] = dateTime;
                row["SaveDirectory"] = saveLocation + @"\FM4B\プロジェクト";
                table.Rows.Add(row);
                string json = JsonConvert.SerializeObject(set, Formatting.Indented);
                sw.Write(json);
                sw.Close();
                Directory.CreateDirectory(saveLocation + @"\FM4B\プロジェクト\" + name);
                return true;
            }
        }

        public List<ProjectInfo> GetProjectInfos()
        {
            string saveLocation = Properties.Settings.Default.saveLocation + @"\FM4B\プロジェクト";
            if (!File.Exists(saveLocation + @"\projects.json"))
                return null;
            StreamReader sr = new StreamReader(saveLocation + @"\projects.json");
            string j = sr.ReadToEnd();
            sr.Close();
            Json json = JsonConvert.DeserializeObject<Json>(j);
            List<ProjectInfo> projects = json.Projects;
            return projects;
        }

        public void ReadProject()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("(プロジェクトを選択してください)");
            List<ProjectInfo> projects = GetProjectInfos();
            if (projects == null)
                return;
            foreach (ProjectInfo pi in projects)
            {
                comboBox1.Items.Add(pi.ProjectName);
            }
        }

        private void Home_Shown(object sender, EventArgs e)
        {
            if (!Directory.Exists(Properties.Settings.Default.saveLocation))
            {
                MessageBox.Show("ファイルの保存先を選択してください。");
                SettingSaveLocation();
            }
            ReadProject();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] tags = { "タグ1", "タグ2", "タグ3" };
            MessageBox.Show(comboBox1.Text);
            AddResource.CreateJsonFile(@"C:\Users\Owner\Desktop\00073.MTS", comboBox1.Text, "これはテストです。", ResourceType.Video, tags, DateTime.Today, false);
            List<FilesAttribute> filesAttributes= AddResource.GetFiles(comboBox1.Text);
            listBox1.Items.Clear();
            foreach (FilesAttribute fa in filesAttributes)
            {
                listBox1.Items.Add(fa.FileName);
            }
        }
    }

    class Json
    {
        public List<ProjectInfo> Projects { get; set; }
    }

    public class ProjectInfo
    {
        public int Number { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string SaveDirectory { get; set; }
    }

}
