﻿using _1DV607A2.Model;
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

        /// <summary>
        /// Creates DataObject, specify model by specifying Type inheriting DataObject. Initialize its data using a dictionary list.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        public void CreateData(Type type, Dictionary<string, object> args)
        {
            var time = DateTime.UtcNow.Ticks;
            var id = CalculateHashID(args) * time;

            DataObject data = (DataObject)type.GetConstructors()[0].Invoke(new object[]{ this, id.ToString(), time });

            if (data != null)
            {
                data.SetNewData(args);
                dataObjects.Add(data);
                fileController.SaveToFile(data);
            }
        }

        /// <summary>
        /// Changes an object's data using a dictionary list
        /// </summary>
        /// <param name="targetID"></param>
        /// <param name="args"></param>
        public void ChangeData(string targetID, Dictionary<string, object> args)
        {
            var obj = RetrieveByID(targetID);
            obj.SetNewData(args);
            fileController.SaveToFile(obj);
        }

        /// <summary>
        /// Deletes an object from both the registry and disk
        /// </summary>
        /// <param name="targetID"></param>
        public void DeleteData(string targetID)
        {
            var obj = RetrieveByID(targetID);

            if (obj != null)
            {
                fileController.DeleteFileFor(obj);
                dataObjects.Remove(obj);
            }
        }

        /// <summary>
        /// Returns an object by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataObject RetrieveByID(string id)
        {
            return dataObjects.Find(data => { return data.ID == id; });
        }

        /// <summary>
        /// Returns an list of objects that satisfy the given boolean selector function
        /// </summary>
        /// <param name="selectorFunction"></param>
        /// <returns></returns>
        public IEnumerable<DataObject> RetrieveByQuery(Func<DataObject,bool> selectorFunction) 
        {
            return dataObjects.Where(selectorFunction);
        }

        /// <summary>
        /// Calculates an unique hash-like identifiers from given dictionary list
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
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
