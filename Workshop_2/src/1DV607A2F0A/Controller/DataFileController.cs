using _1DV607A2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _1DV607A2.Controller
{
    /// <summary>
    /// Controller class for saving, loading, and deleting save-files associated with data objects
    /// </summary>
    class DataFileController
    {
        string directory;

        DataController dataController;

        /// <summary>
        /// Constructor for DataFileController
        /// </summary>
        /// <param name="dataController">DataController dependency to pass down</param>
        /// <param name="workingDirectory">The directory where this object would save or load from</param>
        public DataFileController(DataController dataController, string workingDirectory)
        {
            this.dataController = dataController;
            directory = workingDirectory;
        }

        /// <summary>
        /// Saves given object to a file identified by the object's unique identifier
        /// </summary>
        /// <param name="dataObject">Object to save to file</param>
        public void SaveToFile(DataObject dataObject)
        {
            using (FileStream fs = OpenStream(dataObject))
            {
                var bytes = Encoding.UTF8.GetBytes(dataObject.Serialize());
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
        }

        /// <summary>
        /// Tries to load everything from the saved data directory
        /// </summary>
        /// <param name="objects">Reference list to store the loaded objects in</param>
        public void TryLoadAll(ref List<DataObject> objects)
        {
            if (!Directory.Exists(directory))
                return;

            objects.AddRange(LoadFiles(directory + "/Members",
                (readData) =>
                {
                    var res = new MemberData(
                        dataController, 
                        readData[1], 
                        long.Parse(readData[2]),
                        readData[3],
                        readData[4]
                        );

                    return res;
                }));
            objects.AddRange(LoadFiles(directory + "/Boats",
                (readData) =>
                {
                    var res = new BoatData(
                        dataController,
                        readData[1],
                        long.Parse(readData[2]),
                        (MemberData)dataController.RetrieveByID(readData[3]),
                        int.Parse(readData[4]),
                        (BoatType)Enum.Parse(typeof(BoatType), readData[5])
                        );

                    return res;
                }));
        }

        /// <summary>
        /// Deletes the file associated with given object
        /// </summary>
        /// <param name="dataObject"></param>
        public void DeleteFileFor(DataObject dataObject)
        {
            if (dataObject == null)
                return;

            var path = $"{directory}/{dataObject.DataType}s/{dataObject.ID}.data";
            if (File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// Boilerplate function for reading contents from files and then parsing them to data objects
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="parser"></param>
        /// <returns></returns>
        private List<DataObject> LoadFiles(string folderPath, Func<string[], DataObject> parser)
        {
            var objects = new List<DataObject>();
            var files = new List<string>();

            if (Directory.Exists(folderPath))
            {
                files.AddRange(Directory.GetFiles(folderPath));

                foreach (string file in files)
                {
                    var serializedString = LoadContents(file);

                    var data = serializedString.Split(':');

                    var dataObject = parser.Invoke(data);

                    if (dataObject != null)
                        objects.Add(dataObject);
                }
            }
            return objects; 
        }

        /// <summary>
        /// Load contents from a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new file and opens a stream to write in to
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
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
