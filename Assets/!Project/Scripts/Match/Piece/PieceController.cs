using System.Collections.Generic;
using com.toni.mlin.Core;

namespace com.toni.mlin.Match
{
    public class PieceController : MonoController<PieceController>, IObserver
    {
        public List<PieceView> Pieces { get; private set; }

        public void Register(PieceView piece)
        {
            if (Pieces == null) Pieces = new List<PieceView>();
            Pieces.Add(piece);
        }

        public void Unregister(PieceView piece)
        {
            Pieces.Remove(piece);
        }

        [ObserverMethod]
        private void OnInitiateGame()
        {
            if (Pieces == null)
            {
                Pieces = new List<PieceView>();
                return;
            }

            Pieces.Clear();
        }

        [ObserverMethod]
        private void OnGameExit()
        {
            foreach (var piece in Pieces)
            {
                Destroy(piece.gameObject);
            }

            Pieces.Clear();
        }

        private void Awake()
        {
            MatchController.Instance.Attach(this);
        }
    }
}