using System.Linq;
using com.toni.mlin.Core;
using com.toni.mlin.MainMenu;
using com.toni.mlin.Match.Piece;
using com.toni.mlin.Match.Player;

namespace com.toni.mlin.Match
{
    public class MatchController : MonoController<MatchController>, IObserver
    {
        public PlayerId CurrentPlayerId { get; private set; }
        public MatchConfig MatchConfig { get; private set; }
        public MatchState MatchState { get; private set; }

        public void PiecePlaced()
        {
            this.NextPlayerTurn();
            if (PieceController.Instance.Pieces.Count(piece => piece.Owner == CurrentPlayerId && !piece.Removed) <= MatchConfig.Size)
            {
                MatchState = MatchState.Flying;
            }
            else if (PieceController.Instance.Pieces.All(piece => !piece.InHand))
            {
                MatchState = MatchState.Moving;
            }

            this.NotifyAll("OnPiecePlaced");
        }

        public void PieceRemoved()
        {
            this.NextPlayerTurn();
            if (PieceController.Instance.Pieces.Count(piece => piece.Owner == CurrentPlayerId && !piece.Removed) <= MatchConfig.Size)
            {
                MatchState = MatchState.Flying;
            }
            else if (PieceController.Instance.Pieces.Any(piece => piece.InHand))
            {
                MatchState = MatchState.Placing;
            }
            else
            {
                MatchState = MatchState.Moving;
            }

            this.NotifyAll("OnPieceRemoved");
        }

        private void NextPlayerTurn()
        {
            CurrentPlayerId = CurrentPlayerId == PlayerId.Player1 ? PlayerId.Player2 : PlayerId.Player1;
            if (PieceController.Instance.Pieces.Count(piece => piece.Owner == CurrentPlayerId && !piece.Removed) < 3)
            {
                //parameter represents Winner
                this.NotifyAll("OnGameOver", CurrentPlayerId == PlayerId.Player1 ? PlayerController.Instance.Player2 : PlayerController.Instance.Player1);
                return;
            }
            this.NotifyAll("OnNextPlayerTurn");
        }

        public void MillFound()
        {
            MatchState = MatchState.Removing;
            this.NotifyAll("OnRemovingMatchState");
        }

        public void InitiateGame()
        {
            CurrentPlayerId = PlayerId.Player1;
            MatchState = MatchState.Placing;
            this.NotifyAll("OnInitiateGame");
        }

        public void ExitGame()
        {
            this.NotifyAll("OnGameExit");
        }

        [ObserverMethod]
        private void OnEnterMatchScreen()
        {
            this.InitiateGame();
        }

        private void Start()
        {
            MatchConfig = new MatchConfig(3);
            MainMenuController.Instance.Attach(this);
        }
    }
}