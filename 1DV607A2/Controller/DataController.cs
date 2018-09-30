using _1DV607A2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.Controller
{
    class DataController
    {
        DataFileController fileController;

        List<DataObject> dataObjects;

        public DataController()
        {
            fileController = new DataFileController($"{Environment.CurrentDirectory}/Saved Data");

            dataObjects = fileController.TryLoadAll();
        }

        public void CreateData(Type type, List<string> args)
        {
            DataObject data = null;
            if (type == typeof(MemberData))
            {
                var name = args[0];
                var pn = args[1];
                var time = DateTime.UtcNow.TimeOfDay.TotalMilliseconds;
                var id = name.Sum(c => c) * pn.Sum(c => c) * (long)time;

                var dat = new MemberData(id.ToString(), (long)time);
                dat.Name = name;
                dat.PersonalNumber = pn;
                data = dat;
            }
            else if (type == typeof(BoatData))
            {
                var owner = (MemberData)RetrieveByID(args[0]);
                var boatType = (BoatType)int.Parse(args[1]);
                var length = int.Parse(args[2]);
                var time = DateTime.UtcNow.TimeOfDay.TotalMilliseconds;
                var id =  type.ToString().Sum(c => c) * length * (long)time;

                var dat = new BoatData(id.ToString(), (long)time);
                owner?.RegisterBoat(dat);
                dat.Type = boatType;
                dat.Length = length;
                data = dat;
            }

            if (data != null)
            {
                dataObjects.Add(data);
                fileController.SaveToFile(data);
            }
        }

        public void ChangeData(string targetID, List<string> args)
        {
            
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

        public IEnumerable<TResult> RetrieveByQuery<TResult>(Func<DataObject,TResult> selectorFunction)
        {
            return dataObjects.Select(selectorFunction);
        }
    }
}
