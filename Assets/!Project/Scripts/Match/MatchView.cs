using com.toni.mlin.Core;
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
            this.Show();
            this.boardView.Setup();
            this.boardView.Show();
        }

        private void Start()
        {
            this.Hide();
            MatchController.Instance.Attach(this);
        }
    }
}
