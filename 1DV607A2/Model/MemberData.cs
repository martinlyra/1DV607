using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.Model
{
    public class MemberData : DataObject
    {
        public MemberData(string id, long timestamp) : base(id, timestamp)
        {
            DataType = "Member";
            Boats = new List<BoatData>();
        }

        public void RegisterBoat(BoatData boat)
        {
            boat.Owner = this;
            Boats.Add(boat);
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
