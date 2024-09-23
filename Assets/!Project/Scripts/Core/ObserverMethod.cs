using System;

namespace com.toni.mlin.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ObserverMethod : Attribute
    {
    }
}