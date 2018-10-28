using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.Model
{
    abstract class Observable<T> where T: IObserver
    {
        protected List<T> observers = new List<T>();

        public void AttachObserver(T observer)
        {
            observers.Add(observer);    
        }

        public void DeattachObserver(T observer)
        {
            observers.Remove(observer);
        }

        public void Notify(Action<T> updateAction)
        {
            foreach (T observer in observers)
                updateAction.Invoke(observer);
        }
    }
}
