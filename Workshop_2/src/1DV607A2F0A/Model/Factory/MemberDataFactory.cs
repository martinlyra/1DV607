using _1DV607A2.Controller;

namespace _1DV607A2.Model.Factory
{
    class MemberDataFactory : AbstractDataObjectFactory<MemberData>
    {
        readonly string name;
        readonly string personalNumber;

        public MemberDataFactory(
            DataController dataController,
            string name, 
            string personalNumber)
            : base (dataController)
        {
            this.name = name;
            this.personalNumber = personalNumber;
        }

        public override MemberData Facilitate()
        {
            return new MemberData(dataController, ObjectId, Timestamp, name, personalNumber);
        }
    }
}
