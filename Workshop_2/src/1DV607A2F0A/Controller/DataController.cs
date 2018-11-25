using _1DV607A2.Model;
using _1DV607A2.Model.Factory;
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

        private void AddData<T>(string id, AbstractDataObjectFactory<T> objectFactory) where T : DataObject
        {
            if (id != null && id.Length > 0)
                objectFactory.ObjectId = id;

            DataObject obj = objectFactory.Facilitate();
            dataObjects.Add(obj);
            fileController.SaveToFile(obj);
        }

        public void CreateMemberData(string objectID, string name, string personalNumber)
        {
            AddData(objectID, new MemberDataFactory(this, name, personalNumber));   
        }

        public void CreateBoatData(string objectID, string ownerID, int length, BoatType type)
        {
            MemberData owner = (MemberData)RetrieveByID(ownerID);
            AddData(objectID, new BoatDataFactory(this, owner, length, type));   
        }

        public void SetMemberData(string targetID, string name, string personalNumber)
        {
            MemberData md = (MemberData)RetrieveByID(targetID);
            md.Name = name;
            md.PersonalNumber = personalNumber;
            fileController.SaveToFile(md);
        }

        public void SetBoatData(string targetID, string ownerID, int length, BoatType type)
        {
            BoatData bd = (BoatData)RetrieveByID(targetID);
            if (ownerID != null)
                ((MemberData)RetrieveByID(ownerID)).RegisterBoat(bd);
            bd.Length = length;
            bd.BoatType = type;
            fileController.SaveToFile(bd);
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

        public IEnumerable<MemberData> GetMembers()
        {
            return (IEnumerable<MemberData>)RetrieveByQuery((d) => d is MemberData);
        }

        public IEnumerable<BoatData> GetBoats()
        {
            return (IEnumerable<BoatData>)RetrieveByQuery((d) => d is BoatData);
        }
    }
}
