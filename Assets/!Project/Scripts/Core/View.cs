using System.Linq;
using UnityEngine;

namespace com.toni.mlin.Core
{
    public class View : MonoBehaviour
    {
        public static T[] FindViews<T>() where T : View
        {
            return GameObject.FindObjectsOfType<T>();
        }

        public static T FindView<T>() where T : View
        {
            return FindViews<T>().FirstOrDefault();
        }

        public static T AssertView<T>(T view) where T : View
        {
            return view ?? FindView<T>();
        }

        public static T FindView<T>(string viewID) where T : View
        {
            return FindViews<T>().FirstOrDefault(item => item.GetID() == viewID);
        }

        public static T AssertView<T>(T view, string viewID) where T : View
        {
            return view ?? FindView<T>(viewID);
        }

        public T[] FindViewsInChildren<T>() where T : View
        {
            return this.GetComponentsInChildren<T>(true);
        }

        public T FindViewInChildren<T>() where T : View
        {
            return this.FindViewsInChildren<T>().FirstOrDefault();
        }

        public T FindLastViewInChildren<T>() where T : View
        {
            return this.FindViewsInChildren<T>().LastOrDefault();
        }

        public T AssertViewInChildren<T>(T view) where T : View
        {
            return view ?? this.FindViewInChildren<T>();
        }

        public T FindViewInChildren<T>(string viewID) where T : View
        {
            return this.FindViewsInChildren<T>().FirstOrDefault(item => item.GetID() == viewID);
        }

        public virtual string GetID()
        {
            return string.Empty;
        }
    }
}