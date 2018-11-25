using _1DV607A2.Controller;

namespace _1DV607A2.Model
{
    /// <summary>
    /// Data object model for storing information about boats
    /// </summary>
    public class BoatData : DataObject
    {
        public BoatData(
            DataController dataController, 
            string id, 
            long timestamp,
            MemberData owner,
            int length,
            BoatType type
            ) : 
            base(dataController, id, timestamp)
        {
            DataType = "Boat";

            Length = length;
            BoatType = type;

            if (owner != null)
                owner.RegisterBoat(this);
        }

        public MemberData Owner { get; set; }
        public int Length { get; set; }
        public BoatType BoatType { get; set; }

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