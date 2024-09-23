using com.toni.mlin.Core;
using UnityEngine;

namespace com.toni.mlin.MainMenu
{
    public class MainMenuController : MonoController<MainMenuController>
    {
        public void StartMatch()
        {
            this.NotifyAll("OnStartMatch");
        }

        public void ExitGame()
        {
            Application.Quit(0);
        }
    }
}
