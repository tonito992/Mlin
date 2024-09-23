namespace com.toni.mlin.Core
{
    public abstract class Controller<T> : Singleton<T> where T : class
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