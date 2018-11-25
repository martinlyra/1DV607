using _1DV607A2.Controller;
using System.Collections.Generic;
using System.Linq;

namespace _1DV607A2.Model
{
    /// <summary>
    /// Data object model for storing member data
    /// </summary>
    public class MemberData : DataObject
    {
        public MemberData(
            DataController dataController, 
            string id, 
            long timestamp,
            string name,
            string personalNumber
            ) : 
            base(dataController, id, timestamp)
        {
            DataType = "Member";
            Boats = new List<BoatData>();

            Name = name;
            PersonalNumber = personalNumber;
        }

        /// <summary>
        /// Registers a boat on this member; filling the appropriate details for the boat
        /// </summary>
        /// <param name="boat"></param>
        public void RegisterBoat(BoatData boat)
        {
            if (boat.Owner == this)
                return;
            if (boat.Owner != this)
                boat.Owner?.Boats.Remove(boat);

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
