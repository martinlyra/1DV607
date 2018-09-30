using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.Model
{
    public abstract class DataObject
    {
        public DataObject(string id, long timestamp)
        {
            ID = id;
            Timestamp = timestamp;
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
