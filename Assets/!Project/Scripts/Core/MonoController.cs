using UnityEngine;

namespace com.toni.mlin.Core
{
    public abstract class MonoController<T> : MonoSingleton<T> where T : MonoBehaviour
    {
        private Notifier notifier = new Notifier();

        public void Attach(IObserver observer)
        {
            this.notifier.Attach(observer);
        }

        public void Detach(IObserver observer)
        {
            this.notifier.Detach(observer);
        }

        public void NotifyAll(string notification, params object[] parameters)
        {
            //Debug.Log($"{notification}");
            this.notifier.NotifyAll(notification, parameters);
        }

        public void NotifyOne(string notification, params object[] parameters)
        {
            this.notifier.NotifyOne(notification, parameters);
        }

        public void NotifyCount(string notification, int count, params object[] parameters)
        {
            this.notifier.NotifyCount(notification, count, parameters);
        }
    }
}