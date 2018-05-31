using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using Newtonsoft.Json;

namespace FileManager4Broadcasting
{

    enum ResourceType
    {
        Video,Sound,Image
    }

    static class AddResource
    {
        /*static public void Add(string projectName,string[] resourcePaths,ResourceType resourceType,string[] tags,DateTime importDate)
        {
            string saveLocation = Properties.Settings.Default.saveLocation + @"\FM4B\プロジェクト\"+projectName;
            DupFilesForm dff = new DupFilesForm();
            // なかった際のディレクトリ作成
            if (!Directory.Exists(saveLocation + @"\画像"))
            {
                Directory.CreateDirectory(saveLocation + @"\画像");
            }
            if (!Directory.Exists(saveLocation + @"\映像"))
            {
                Directory.CreateDirectory(saveLocation + @"\映像");
            }
            if(!Directory.Exists(saveLocation + @"\音声"))
            {
                Directory.CreateDirectory(saveLocation + @"\音声");
            }

            switch (resourceType)
            {
                case ResourceType.Video:
                    dff.dupLocation = saveLocation + @"\映像";
                    dff.files = resourcePaths;
                    dff.ShowDialog();
                    return;
                case ResourceType.Sound:
                    dff.dupLocation = saveLocation + @"\音声";
                    dff.files = resourcePaths;
                    dff.ShowDialog();
                    return;
                case ResourceType.Image:
                    dff.dupLocation = saveLocation + @"\画像";
                    dff.files = resourcePaths;
                    dff.ShowDialog();
                    return;
            }
        }*/
        static  public List<FilesAttribute> GetFiles(string projectName)
        {
            string saveLocation = Properties.Settings.Default.saveLocation + @"\FM4B\プロジェクト\"+projectName;
            if (!File.Exists(saveLocation + @"\files.json"))
                return null;
            StreamReader sr = new StreamReader(saveLocation + @"\files.json");
            string j = sr.ReadToEnd();
            sr.Close();
            FilesJson json = JsonConvert.DeserializeObject<FilesJson>(j);
            List<FilesAttribute> files = json.Files;
            return files;
        }

        static public FilesAttribute GetFile(string projectName,int n)
        {
            string saveLocation = Properties.Settings.Default.saveLocation + @"\FM4B\プロジェクト\" + projectName;
            if (!File.Exists(saveLocation + @"\files.json"))
                return null;
            StreamReader sr = new StreamReader(saveLocation + @"\files.json");
            string j = sr.ReadToEnd();
            sr.Close();
            FilesJson json = JsonConvert.DeserializeObject<FilesJson>(j);
            List<FilesAttribute> files = json.Files;
            foreach (FilesAttribute fa in files)
            {
                if (fa.Number == n)
                {
                    return fa;
                }
            }
            return null;
        }

        static public void CreateJsonFile(string path,string projectName,string d,ResourceType type,string[] _tags,DateTime cDate,bool _isLinked)
        {
            string saveLocation = Properties.Settings.Default.saveLocation + @"\FM4B\プロジェクト\" + projectName;
            List<FilesAttribute> files = GetFiles(projectName);

            DataSet set = new DataSet();
            DataTable table = new DataTable("Files");
            DataColumn number = new DataColumn("Number", typeof(int));
            number.AutoIncrement = true;
            DataColumn fileName = new DataColumn("FileName", typeof(string));
            DataColumn filePath = new DataColumn("FilePath", typeof(string));
            DataColumn descri = new DataColumn("Description", typeof(string));
            DataColumn resourceType = new DataColumn("ResourceType", typeof(string));
            DataColumn tags = new DataColumn("Tags", typeof(string[]));
            DataColumn importedDate = new DataColumn("ImportedDate", typeof(DateTime));
            DataColumn createdDate = new DataColumn("CreatedDate", typeof(DateTime));
            DataColumn isLinked = new DataColumn("IsLinked", typeof(bool));
            table.Columns.Add(number);
            table.Columns.Add(fileName);
            table.Columns.Add(filePath);
            table.Columns.Add(descri);
            table.Columns.Add(resourceType);
            table.Columns.Add(tags);
            table.Columns.Add(importedDate);
            table.Columns.Add(createdDate);
            table.Columns.Add(isLinked);
            set.Tables.Add(table);
            if (files != null)
            {
                foreach (FilesAttribute fa in files)
                {
                    DataRow r = table.NewRow();
                    r["FileName"] = fa.FileName;
                    r["FilePath"] = fa.FilePath;
                    r["Description"] = fa.Description;
                    r["ResourceType"] = fa.ResourceType;
                    r["Tags"] = fa.Tags;
                    r["ImportedDate"] = fa.ImportedDate;
                    r["CreatedDate"] = fa.CreatedDate;
                    r["isLinked"] = fa.IsLinked;
                    table.Rows.Add(r);
                }
            }
            {
                DataRow r = table.NewRow();
                r["FileName"] = Path.GetFileName(path);
                r["FilePath"] = path;
                r["Description"] = d;
                r["ResourceType"] = type.ToString();
                r["Tags"] = _tags;
                r["ImportedDate"] = DateTime.Today;
                r["CreatedDate"] = cDate;
                r["isLinked"] = _isLinked;
                table.Rows.Add(r);
                string j = JsonConvert.SerializeObject(set, Formatting.Indented);
                StreamWriter s = new StreamWriter(saveLocation + @"\files.json", false);
                s.Write(j);
                s.Close();
            }
        }

    }
    

    class FilesJson
    {
        public List<FilesAttribute> Files { get; set; }
    }

    public class FilesAttribute
    {
        public int Number { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string ResourceType { get; set; }
        public string[] Tags { get; set; }
        public DateTime ImportedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsLinked { get; set; }
    }

}
