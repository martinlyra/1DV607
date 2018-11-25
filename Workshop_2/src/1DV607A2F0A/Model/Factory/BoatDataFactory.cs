using _1DV607A2.Controller;

namespace _1DV607A2.Model.Factory
{
    class BoatDataFactory : AbstractDataObjectFactory<BoatData>
    {
        private readonly MemberData member;
        private readonly int length;
        private readonly BoatType type;

        public BoatDataFactory(
            DataController dataController, 
            MemberData member, 
            int length, 
            BoatType type) : 
            base(dataController)
        {
            this.member = member;
            this.length = length;
            this.type = type;
        }

        public override BoatData Facilitate()
        {
            return new BoatData(dataController, ObjectId, Timestamp, member, length, type);    
        }
    }
}
