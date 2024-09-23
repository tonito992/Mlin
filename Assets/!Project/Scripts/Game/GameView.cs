using com.toni.mlin.Core;
using com.toni.mlin.MainMenu;
using com.toni.mlin.Match.Board;
using UnityEngine;

namespace com.toni.mlin.Game
{
    public class GameView : UIView, IObserver
    {
        [SerializeField] private BoardView boardView;

        [ObserverMethod]
        private void OnStartMatch()
        {
            this.boardView.Setup();
            this.boardView.Show();
        }

        private void Awake()
        {
            MainMenuController.Instance.Attach(this);
        }
    }
}
