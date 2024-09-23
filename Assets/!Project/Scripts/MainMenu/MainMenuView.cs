using com.toni.mlin.Core;

namespace com.toni.mlin.MainMenu
{
    public class MainMenuView : UIView
    {
        public void OnPressNewMatch()
        {
            MainMenuController.Instance.StartMatch();
            this.Hide();
        }

        public void OnPressExitGame()
        {
            MainMenuController.Instance.ExitGame();
        }
    }
}
