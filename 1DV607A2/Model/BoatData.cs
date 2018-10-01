using _1DV607A2.Controller;
using System.Collections.Generic;

namespace _1DV607A2.Model
{
    public class BoatData : DataObject
    {
        public BoatData(DataController dataController, string id, long timestamp) : base(dataController, id, timestamp)
        {
            DataType = "Boat";
        }

        public MemberData Owner { get; set; }
        public int Length { get; set; }
        public BoatType Type { get; set; }

        public override void ChangeData(Dictionary<string, object> newData)
        {
            if (newData.ContainsKey("owner"))
            {
                var newOwner = (MemberData)dataController.RetrieveByID((string)newData["owner"]);
                newOwner.RegisterBoat(this);
            }
            if (newData.ContainsKey("length")) 
                Length = (int)newData["length"];

            if (newData.ContainsKey("type"))
                Type = (BoatType)newData["type"];
        }

        public override void Delete()
        {
            Owner.Boats.Remove(this);
        }

        public override string Serialize()
        {
            return base.Serialize() + $":{Owner.ID}:{Length}:{Type.ToString()}";
        }
    }
}