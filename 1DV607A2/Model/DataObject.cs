using _1DV607A2.Controller;
using System.Collections.Generic;

namespace _1DV607A2.Model
{
    public abstract class DataObject
    {
        protected readonly DataController dataController;

        public DataObject(DataController dataController, string id, long timestamp)
        {
            this.dataController = dataController;
            ID = id;
            Timestamp = timestamp;
        }

        public virtual void ChangeData(Dictionary<string, object> newData)
        {
            if (newData.ContainsKey("id"))
                ID = (string)newData["id"];
            if (newData.ContainsKey("timestamp"))
                Timestamp = (long)newData["timestamp"];
        }

        public abstract void Delete();

        public virtual string Serialize()
        {
            return $"{DataType}:{ID}:{Timestamp}";
        }

        public string ID { get; protected set; }
        public long Timestamp { get; protected set; }
        public string DataType { get; protected set; }
    }
}
