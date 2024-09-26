using com.toni.mlin.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace com.toni.mlin.Match
{
    public class GameOverView : UIView, IObserver
    {
        [SerializeField] private GameObject popUp;
        [SerializeField] private TMP_Text textWinnerName;

        public override void Show()
        {
            base.Show();
            this.popUp.transform.localScale = Vector3.zero;
            this.popUp.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InCubic);
        }

        public void OnTapRestart()
        {
            this.Hide();
            MatchController.Instance.ExitGame();
            MatchController.Instance.InitiateGame();
        }

        public void OnTapMainMenu()
        {
            this.Hide();
            MatchController.Instance.ExitGame();
        }

        [ObserverMethod]
        private void OnGameOver(Player.Player winner)
        {
            this.Show();
            this.textWinnerName.SetText(winner.Name);
        }

        private void Awake()
        {
            MatchController.Instance.Attach(this);
        }
    }
}