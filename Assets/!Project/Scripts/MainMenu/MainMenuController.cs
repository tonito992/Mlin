using com.toni.mlin.Core;
using UnityEngine;

namespace com.toni.mlin.MainMenu
{
    public class MainMenuController : MonoController<MainMenuController>
    {
        public void OpenMatchScreen()
        {
            this.NotifyAll("OnEnterMatchScreen");
        }

        public void ExitGame()
        {
            Application.Quit(0);
        }
    }
}
