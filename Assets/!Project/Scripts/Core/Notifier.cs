using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace com.toni.mlin.Core
{
    public sealed class Notifier : IDisposable
    {
        private HashSet<ObserverWrapper> observers;

        public Notifier()
        {
            this.observers = new HashSet<ObserverWrapper>();
        }

        public void Attach(IObserver observer)
        {
            this.observers.Add(new ObserverWrapper(observer));
        }

        public void Detach(IObserver observer)
        {
            this.observers.RemoveWhere(obs => obs.GetHashCode() == observer.GetHashCode());
        }

        public void NotifyAll(string notification, params object[] parameters)
        {
            this.Notify(
                notification,
                (method, observer) =>
                {
                    method.Invoke(observer, parameters);
                    return false;
                });
        }

        public void NotifyOne(string notification, params object[] parameters)
        {
            this.Notify(
                notification,
                (method, observer) =>
                {
                    object result = method.Invoke(observer, parameters);
                    if (result is ConsumeFlag && (ConsumeFlag)result == ConsumeFlag.CONSUME)
                    {
                        return true;
                    }

                    return false;
                });
        }

        public void NotifyCount(string notification, int count, params object[] parameters)
        {
            this.Notify(
                notification,
                (method, observer) =>
                {
                    object result = method.Invoke(observer, parameters);
                    count = result is ConsumeFlag && (ConsumeFlag)result == ConsumeFlag.CONSUME ? count - 1 : count;
                    return count <= 0;
                });
        }

        public void Dispose()
        {
            this.observers.Clear();
        }

        private void Notify(string notification, Func<MethodInfo, IObserver, bool> action)
        {
            int hash = notification.GetHashCode();
            ObserverWrapper[] observers = this.observers.ToArray();
            for (int i = 0; i < observers.Length; i++)
            {
                ObserverWrapper observer = observers[i];
                if (observer.Observer.Equals(default(IObserver)))
                {
                    this.observers.Remove(observer);
                    continue;
                }

                if (observer.Methods.ContainsKey(hash))
                {
                    action(observer.Methods[hash], observer.Observer);
                }
            }
        }

        private struct ObserverWrapper
        {
            private int hash;

            public ObserverWrapper(IObserver observer)
            {
                this.hash = observer.GetHashCode();
                this.Observer = observer;
                this.Methods = new Dictionary<int, MethodInfo>();
                MethodInfo[] methodInfos = observer.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(method => method.GetCustomAttribute<ObserverMethod>() != null).ToArray();
                for (int i = 0; i < methodInfos.Length; i++)
                {
                    MethodInfo info = methodInfos[i];
                    this.Methods.Add(info.Name.GetHashCode(), info);
                }
            }

            public IObserver Observer { get; private set; }

            public Dictionary<int, MethodInfo> Methods { get; private set; }

            public override int GetHashCode()
            {
                return this.hash;
            }
        }
    }
}