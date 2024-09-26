using com.toni.mlin.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.toni.mlin.Match.Board
{
    public class NodeView : MonoBehaviour, IPointerDownHandler, IObserver
    {
        [SerializeField] private GameObject availableIndicator;

        private int offset = 64;

        public Node Model { get; private set; }

        public RectTransform RectTransform { get; private set; }

        public void Setup(Node nodeModel)
        {
            RectTransform = this.GetComponent<RectTransform>();
            Model = nodeModel;
            var x = Model.Coordinates.x;
            var y = Model.Coordinates.y;
            var depth = Model.Depth;

            RectTransform.anchoredPosition = new Vector2(-this.offset * (depth + 1) + x * this.offset * (depth + 1),
                -this.offset * (depth + 1) + y * this.offset * (depth + 1));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Model.Occupant != PlayerId.None) return;

            var selectedPiece = PlayerController.Instance.SelectedPiece;
            if (selectedPiece != null)
            {
                Model.Occupy(selectedPiece.Owner);
                selectedPiece.PlaceOnNode(this);
                this.availableIndicator.SetActive(false);
            }
        }

        [ObserverMethod]
        private void OnPieceSelected(PieceView selectedPiece)
        {
            if (MatchController.Instance.MatchState == MatchState.Moving)
            {
                this.availableIndicator.SetActive(BoardController.IsNeighbor(Model, selectedPiece.CurrentNode));
            }
            else if (MatchController.Instance.MatchState == MatchState.Flying && Model.Occupant == PlayerId.None)
            {
                this.availableIndicator.SetActive(true);
            }
        }

        [ObserverMethod]
        private void OnPieceDeselected()
        {
            this.availableIndicator.SetActive(false);
        }

        private void Awake()
        {
            PlayerController.Instance.Attach(this);
        }
    }
}