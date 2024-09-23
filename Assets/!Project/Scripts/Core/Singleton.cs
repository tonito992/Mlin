using System;
using System.Reflection;

namespace com.toni.mlin.Core
{
    public abstract class Singleton<T> where T : class
    {
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(() =>
        {
            ConstructorInfo[] constructors = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            ConstructorInfo[] publicConstructors = typeof(T).GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (publicConstructors.Length > 0)
            {
                throw new Exception("Singleton can not have public constructors.");
            }

            if (Array.Exists(constructors, constructor => constructor.GetParameters().Length != 0))
            {
                throw new Exception("Singleton can not have constructors with parameters.");
            }

            ConstructorInfo emptyConstructor = Array.Find(constructors, constructor => constructor.IsPrivate && constructor.GetParameters().Length == 0);
            return emptyConstructor.Invoke(new object[] { }) as T;
        });

        public static T Instance
        {
            get
            {
                return LazyInstance.Value;
            }
        }
    }
}