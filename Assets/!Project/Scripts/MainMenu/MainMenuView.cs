using com.toni.mlin.Core;
using UnityEngine;

namespace com.toni.mlin.MainMenu
{
    public class MainMenuView : MonoBehaviour, IView
    {
        [SerializeField] private GameObject container;

        public void OnPressNewMatch()
        {
        }

        public void OnPressExitGame()
        {

        }

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
