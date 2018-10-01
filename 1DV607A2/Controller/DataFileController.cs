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

        DataController dataController;

        public DataFileController(DataController dataController, string workingDirectory)
        {
            this.dataController = dataController;
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

        public void TryLoadAll(ref List<DataObject> objects)
        {
            objects.AddRange(LoadFiles(directory + "/Members",
                (readData) =>
                {
                    var res = new MemberData(dataController, readData[1], long.Parse(readData[2]));
                    res.Name = readData[3];
                    res.PersonalNumber = readData[4];

                    return res;
                }));
            objects.AddRange(LoadFiles(directory + "/Boats",
                (readData) =>
                {
                    var res = new BoatData(dataController, readData[1], long.Parse(readData[2]));

                    var owner = (MemberData)dataController.RetrieveByID(readData[3]);
                    owner.RegisterBoat(res);

                    res.Length = int.Parse(readData[4]);
                    res.Type = (BoatType)Enum.Parse(typeof(BoatType), readData[5]);

                    return res;
                }));
        }

        public void DeleteFileFor(DataObject dataObject)
        {
            if (dataObject == null)
                return;

            var path = $"{directory}/{dataObject.DataType}s/{dataObject.ID}.data";
            if (File.Exists(path))
                File.Delete(path);
        }

        private List<DataObject> LoadFiles(string folderPath, Func<string[], DataObject> parser)
        {
            var objects = new List<DataObject>();
            var files = new List<string>();
            files.AddRange(Directory.GetFiles(folderPath));

            foreach (string file in files)
            {
                var serializedString = LoadContents(file);

                var data = serializedString.Split(':');

                var dataObject = parser.Invoke(data);

                if (dataObject != null)
                    objects.Add(dataObject);
            }
            return objects; 
        }

        private void LoadBoats(string folderPath)
        {

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
