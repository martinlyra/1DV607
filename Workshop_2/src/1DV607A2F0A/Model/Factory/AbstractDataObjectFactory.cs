using _1DV607A2.Controller;
using System;

namespace _1DV607A2.Model.Factory
{
    abstract class AbstractDataObjectFactory<T> where T: DataObject
    {
        protected readonly DataController dataController;

        protected AbstractDataObjectFactory(DataController dataController)
        {
            this.dataController = dataController;

            Timestamp = DateTime.UtcNow.Ticks;

            ObjectId = Guid.NewGuid().ToString();
        }

        abstract public T Facilitate();

        public string ObjectId { get; set; }
        public long Timestamp { get; set; }
    }
}
