using com.toni.mlin.Core;
using TMPro;
using UnityEngine;

namespace com.toni.mlin.Match.Player
{
    public class PlayerView : UIView, IObserver
    {
        [SerializeField] private PlayerId playerId;
        [SerializeField] private TMP_Text playerName;
        [SerializeField] private HandView handView;

        private Player player;

        private void Setup()
        {
            this.player = this.playerId == PlayerId.Player1 ? PlayerController.Instance.Player1 : PlayerController.Instance.Player2;
            this.handView.Setup(this.player);
            this.playerName.SetText(this.player.Name);
        }

        [ObserverMethod]
        private void OnInitiateGame()
        {
            this.Setup();
        }

        private void Awake()
        {
            MatchController.Instance.Attach(this);
        }
    }
}