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
        static public void Add(string projectName,string[] resourcePaths,ResourceType resourceType,string[] tags,DateTime importDate)
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
        }

        static void CreateJsonFile(string[] rPaths,string d,ResourceType type,string[] t,DateTime iDate)
        {
            DataSet set = new DataSet();
            DataTable table = new DataTable("Files");
            DataColumn number = new DataColumn("Number", typeof(int));
            number.AutoIncrement = true;
            DataColumn fileName = new DataColumn("FileName", typeof(string));
            DataColumn filePath = new DataColumn("FilePath", typeof(string));
            DataColumn descri = new DataColumn("Description", typeof(string));
            DataColumn resourceType = new DataColumn("ResourceType", typeof(string));
            DataColumn tags = new DataColumn("Tags", typeof(string[]));
            DataColumn importDate = new DataColumn("ImportDate", typeof(DateTime));
            DataColumn createdDate = new DataColumn("CreatedDate", typeof(DateTime));
            DataColumn isLinked = new DataColumn("IsLinked", typeof(bool));
            table.Columns.Add(number);
            table.Columns.Add(fileName);
            table.Columns.Add(filePath);
            table.Columns.Add(descri);
            table.Columns.Add(resourceType);
            table.Columns.Add(tags);
            table.Columns.Add(importDate);
            table.Columns.Add(createdDate);
            table.Columns.Add(isLinked);
            foreach (string s in rPaths)
            {
                DataRow r = table.NewRow();
                r["FileName"] = Path.GetFileName(s);
                r["FilePath"] = s;
                r["Description"] = d;
                switch (type)
                {
                    case ResourceType.Video:
                        r["ResourceType"] = "Video";
                        return;
                    case ResourceType.Sound:
                        r["ResourceType"] = "Sound";
                        return;
                    case ResourceType.Image:
                        r["ResourceType"] = "Image";
                        return;
                }
                //まだまだあるよ05/29の自分より。
            }
        }

    }
    
    class FilesJson
    {
        List<FilesAttribute> Files { get; set; }
    }

    class FilesAttribute
    {
        int Number { get; set; }
        string FileName { get; set; }
        string FilePath { get; set; }
        string Description { get; set; }
        string ResourceType { get; set; }
        string[] Tags { get; set; }
        DateTime ImportDate { get; set; }
        DateTime CreatedDate { get; set; }
        bool IsLinked { get; set; }
    }

}
