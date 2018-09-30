namespace _1DV607A2.Model
{
    public class BoatData : DataObject
    {
        public BoatData(string id, long timestamp) : base(id, timestamp)
        {
            DataType = "Boat";
        }

        public MemberData Owner { get; set; }
        public int Length { get; set; }
        public BoatType Type { get; set; }

        public override void Delete()
        {
            Owner.Boats.Remove(this);
        }

        public override string Serialize()
        {
            return base.Serialize() + $":{Owner}:{Length}:{Type.ToString()}";
        }
    }
}