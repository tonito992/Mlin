using com.toni.mlin.Core;
using com.toni.mlin.Match.Piece;
using UnityEngine;

namespace com.toni.mlin.Match.Player
{
    public class PlayerController : MonoController<PlayerController>, IObserver
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public PieceView SelectedPiece { get; private set; }

        public void SelectPiece(PieceView piece)
        {
            if(SelectedPiece != null)
            {
                if (piece == SelectedPiece) return;

                SelectedPiece.Deselect();
                SelectedPiece = null;
            }

            SelectedPiece = piece;
            this.NotifyAll("OnPieceSelected", SelectedPiece);
        }

        public void DeselectPiece()
        {
            SelectedPiece = null;
            this.NotifyAll("OnPieceDeselected");
        }

        private void Start()
        {
            Player1 = new Player(PlayerId.Player1, "P1", Color.white);
            Player2 = new Player(PlayerId.Player2, "P2", new Color(.18f, .18f, .18f, 1));
        }
    }
}