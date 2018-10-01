using _1DV607A2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1DV607A2.Controller
{
    public class DataController
    {
        DataFileController fileController;

        List<DataObject> dataObjects;

        public DataController()
        {
            fileController = new DataFileController(this, $"{Environment.CurrentDirectory}/Saved Data");

            dataObjects = new List<DataObject>();
            fileController.TryLoadAll(ref dataObjects);
        }

        public void CreateData(Type type, Dictionary<string, object> args)
        {
            var time = DateTime.UtcNow.Ticks;
            var id = CalculateHashID(args) * time;

            DataObject data = (DataObject)type.GetConstructors()[0].Invoke(new object[]{ this, id.ToString(), time });

            if (data != null)
            {
                data.ChangeData(args);
                dataObjects.Add(data);
                fileController.SaveToFile(data);
            }
        }

        public void ChangeData(string targetID, Dictionary<string, object> args)
        {
            var obj = RetrieveByID(targetID);
            obj.ChangeData(args);
            fileController.SaveToFile(obj);
        }

        public void DeleteData(string targetID)
        {
            var obj = RetrieveByID(targetID);

            if (obj != null)
            {
                fileController.DeleteFileFor(obj);
                dataObjects.Remove(obj);
            }
        }

        public DataObject RetrieveByID(string id)
        {
            return dataObjects.Find(data => { return data.ID == id; });
        }

        public IEnumerable<DataObject> RetrieveByQuery(Func<DataObject,bool> selectorFunction) 
        {
            return dataObjects.Where(selectorFunction);
        }

        private long CalculateHashID(Dictionary<string, object> args)
        {
            long result = 1;
            foreach (object obj in args.Values)
            {
                result *= obj.GetHashCode();
            }
            return result;
        }
    }
}
