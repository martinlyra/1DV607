using _1DV607A2.Controller;
using System.Collections.Generic;

namespace _1DV607A2.Model
{
    /// <summary>
    /// Abstract model for data objects
    /// </summary>
    public abstract class DataObject
    {
        protected readonly DataController dataController;

        public DataObject(DataController dataController, string id, long timestamp)
        {
            this.dataController = dataController;
            ID = id;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Bulk-change data using dictionaries
        /// </summary>
        /// <param name="newData"></param>
        public virtual void SetNewData(Dictionary<string, object> newData)
        {
            if (newData.ContainsKey("id"))
                ID = (string)newData["id"];
            if (newData.ContainsKey("timestamp"))
                Timestamp = (long)newData["timestamp"];
        }

        /// <summary>
        /// Function for handling deletion of objects, for proper clean object-oriented structure
        /// </summary>
        public abstract void Delete();

        /// <summary>
        /// Serializes the object; saving the data down to a string that can be used in sharing over medium or saving to disk
        /// </summary>
        /// <returns></returns>
        public virtual string Serialize()
        {
            return $"{DataType}:{ID}:{Timestamp}";
        }

        public string ID { get; protected set; }
        public long Timestamp { get; protected set; }
        public string DataType { get; protected set; }
    }
}
