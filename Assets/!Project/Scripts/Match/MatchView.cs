using com.toni.mlin.Core;
using UnityEngine;

namespace com.toni.mlin.Match
{
    public class MatchView : MonoBehaviour, IView
    {
        [SerializeField] private GameObject container;

        public void Show()
        {
            this.container.SetActive(true);
        }

        public void Hide()
        {
            this.container.SetActive(false);
        }
    }
}