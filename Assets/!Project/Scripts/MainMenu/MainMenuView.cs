using System;
using com.toni.mlin.Core;

namespace com.toni.mlin.MainMenu
{
    public class MainMenuView : UIView
    {
        public void OnPressNewMatch()
        {
            MainMenuController.Instance.OpenMatchScreen();
            this.Hide();
        }

        public void OnPressExitGame()
        {
            MainMenuController.Instance.ExitGame();
        }

        private void Start()
        {
            this.Show();
        }
    }
}
