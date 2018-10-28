using _1DV607A2.Controller;
using System.Collections.Generic;

namespace _1DV607A2.Model
{
    /// <summary>
    /// Data object model for storing information about boats
    /// </summary>
    public class BoatData : DataObject
    {
        public BoatData(DataController dataController, string id, long timestamp) : base(dataController, id, timestamp)
        {
            DataType = "Boat";
        }

        public MemberData Owner { get; set; }
        public int Length { get; set; }
        public BoatType BoatType { get; set; }

        public override void SetNewData(Dictionary<string, object> newData)
        {
            if (newData.ContainsKey("owner"))
            {
                var newOwner = (MemberData)dataController.RetrieveByID((string)newData["owner"]);
                newOwner.RegisterBoat(this);
            }
            if (newData.ContainsKey("length")) 
                Length = (int)newData["length"];

            if (newData.ContainsKey("boat-type"))
                BoatType = (BoatType)newData["boat-type"];
        }

        public override void Delete()
        {
            Owner.Boats.Remove(this);
        }

        public override string Serialize()
        {
            return base.Serialize() + $":{Owner.ID}:{Length}:{BoatType.ToString()}";
        }
    }
}