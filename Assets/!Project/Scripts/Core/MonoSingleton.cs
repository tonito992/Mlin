using UnityEngine;

namespace com.toni.mlin.Core
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly object Padlock = new object();
        private static T instance;
        private static bool applicationIsQuitting = false;

        public static T Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (instance == null)
                    {
                        instance = GameObject.FindObjectOfType<T>();
                        if (instance != null)
                        {
                            if (GameObject.FindObjectsOfType<T>().Length > 1)
                            {
                                UnityEngine.Debug.LogErrorFormat("Multiple instances of {0}", nameof(T).ToString());
                            }

                            return instance;
                        }

                        if (applicationIsQuitting)
                        {
                            return null;
                        }

                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = string.Format("(singleton) {0}", typeof(T).ToString());
                        UnityEngine.Debug.LogWarning(singleton.name);
                    }

                    return instance;
                }
            }
        }

        private void OnApplicationQuit()
        {
            applicationIsQuitting = true;
        }
    }
}