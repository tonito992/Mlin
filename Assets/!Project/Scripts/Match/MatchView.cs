using com.toni.mlin.Core;
using com.toni.mlin.MainMenu;
using com.toni.mlin.Match.Board;
using UnityEngine;

namespace com.toni.mlin.Match
{
    public class MatchView : UIView, IObserver
    {
        [SerializeField] private BoardView boardView;

        [ObserverMethod]
        private void OnInitiateGame()
        {
            this.boardView.Setup();
            this.boardView.Show();
        }

        [ObserverMethod]
        private void OnEnterMatchScreen()
        {
            this.Show();
        }

        private void Awake()
        {
            this.Hide();
            MatchController.Instance.Attach(this);
            MainMenuController.Instance.Attach(this);
        }
    }
}
