using _1DV607A2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.Controller
{
    class DataFileController
    {
        string directory;

        public DataFileController(string workingDirectory)
        {
            directory = workingDirectory;
        }

        public void SaveToFile(DataObject dataObject)
        {
            using (FileStream fs = OpenStream(dataObject))
            {
                var bytes = Encoding.UTF8.GetBytes(dataObject.Serialize());
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
        }

        public List<DataObject> TryLoadAll()
        {
            List<DataObject> objects = new List<DataObject>();

            var dirs = Directory.GetDirectories(directory);
            List<string> files = new List<string>();

            foreach (string dir in dirs)
                files.AddRange(Directory.GetFiles(dir));

            foreach (string file in files)
            {
                var serializedString = LoadContents(file);

                var data = serializedString.Split(':');
                var type = data[0];

                DataObject dataObject = null;
                if (type == "Member")
                {
                    var md = new MemberData(data[1], long.Parse(data[2]));
                    md.Name = data[3];
                    md.PersonalNumber = data[4];

                    dataObject = md;
                }
                else if (type == "Boat")
                {
                    var bd = new BoatData(data[1], long.Parse(data[2]));
                    bd.Length = int.Parse(data[4]);

                    dataObject = bd;
                }

                if (dataObject != null)
                    objects.Add(dataObject);
            }

            return objects;
        }

        public void DeleteFileFor(DataObject dataObject)
        {
            if (dataObject == null)
                return;

            var path = $"{directory}/{dataObject.DataType}s/{dataObject.ID}.data";
            if (File.Exists(path))
                File.Delete(path);
        }

        private string LoadContents(string path)
        {
            string result;

            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                byte[] bytes = new byte[fs.Length];
                while (fs.Position < fs.Length)
                    bytes[fs.Position] = (byte)fs.ReadByte();

                fs.Close();

                result = Encoding.UTF8.GetString(bytes);
            }

            return result;
        }

        private FileStream OpenStream(DataObject dataObject)
        {
            var dir = $"{directory}/{dataObject.DataType}s/";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);


            var path = dir + $"{ dataObject.ID }.data";
            return File.Open(path, FileMode.Create);
        }
    }
}
