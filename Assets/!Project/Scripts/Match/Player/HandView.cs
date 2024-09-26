using System.Collections.Generic;
using System.Linq;
using com.toni.mlin.Core;
using UnityEngine;

namespace com.toni.mlin.Match
{
    public class HandView : MonoBehaviour, IObserver
    {
        [SerializeField] private PieceView piecePrefab;
        [SerializeField] private Transform piecesParent;

        private List<PieceView> pieces = new();
        private PlayerId playerId;

        public void Setup(Player player)
        {
            this.pieces.Clear();

            this.playerId = player.PlayerId;
            var piecesAmount = MatchController.Instance.MatchConfig.Size * 3;
            for (int i = 0; i < piecesAmount; i++)
            {
                var newPiece = Instantiate(this.piecePrefab, this.piecesParent);
                newPiece.gameObject.SetActive(true);
                newPiece.Setup(player);
                this.pieces.Add(newPiece);
                PieceController.Instance.Register(newPiece);
            }

            if(player.PlayerId == PlayerId.Player1) this.SelectNextHandPiece();
        }

        private void SelectNextHandPiece()
        {
            this.pieces.RemoveAll(piece => piece.CurrentNode != null);
            if (this.pieces.Count == 0) return;
            var piece = this.pieces.Last();
            piece.Select();
            PlayerController.Instance.SelectPiece(piece);
        }

        [ObserverMethod]
        private void OnPiecePlaced()
        {
            if (this.pieces.Count == 0) return;
            if (MatchController.Instance.CurrentPlayerId != this.playerId) return;
            this.SelectNextHandPiece();
        }

        [ObserverMethod]
        private void OnPieceRemoved()
        {
            this.pieces.RemoveAll(piece => piece.Removed);
            if (MatchController.Instance.CurrentPlayerId != this.playerId) return;
            if (MatchController.Instance.MatchState != MatchState.Placing) return;
            this.SelectNextHandPiece();
        }

        private void Awake()
        {
            MatchController.Instance.Attach(this);
        }
    }
}