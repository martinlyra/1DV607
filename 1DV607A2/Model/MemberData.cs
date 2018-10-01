using _1DV607A2.Controller;
using System.Collections.Generic;
using System.Linq;

namespace _1DV607A2.Model
{
    public class MemberData : DataObject
    {
        public MemberData(DataController dataController, string id, long timestamp) : base(dataController, id, timestamp)
        {
            DataType = "Member";
            Boats = new List<BoatData>();
        }

        public void RegisterBoat(BoatData boat)
        {
            if (boat.Owner != this)
                boat.Owner?.Boats.Remove(boat);
            boat.Owner = this;
            Boats.Add(boat);
        }

        public override void ChangeData(Dictionary<string, object> newData)
        {
            if (newData.ContainsKey("name"))
                Name = (string)newData["name"];

            if (newData.ContainsKey("personl-num")) ;
                PersonalNumber = (string)newData["personal-num"];
        }

        public string Name { get; set; }
        public string PersonalNumber { get; set; }
        public List<BoatData> Boats { get; private set; }

        public override string Serialize()
        {
            return base.Serialize() + $":{Name}:{PersonalNumber}:{string.Join(",", Boats.Select(boat => boat.ID))}";
        }

        public override void Delete()
        {
            foreach (BoatData boat in Boats)
                boat.Owner = null;
        }
    }
}
