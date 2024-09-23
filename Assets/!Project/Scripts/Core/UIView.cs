using System.Collections.Generic;
using UnityEngine;

namespace com.toni.mlin.Core
{
    public abstract class UIView : View
    {
        [SerializeField] protected GameObject container;

        public virtual void Show()
        {
            this.container.SetActive(true);
        }

        public virtual void Hide()
        {
            this.container.SetActive(false);
        }

        public T GetItem<T>(int index, T item, List<T> list) where T : Component
        {
            if (index < list.Count)
            {
                return list[index];
            }

            T newItem = GameObject.Instantiate(item, item.gameObject.transform.parent);
            list.Add(newItem);
            return newItem;
        }

        public GameObject GetItem(int index, GameObject item, List<GameObject> list)
        {
            if (index < list.Count)
            {
                return list[index];
            }

            GameObject newItem = GameObject.Instantiate(item, item.transform.parent);
            list.Add(newItem);
            return newItem;
        }
    }
}
