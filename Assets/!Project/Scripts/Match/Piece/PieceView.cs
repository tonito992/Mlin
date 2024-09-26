using com.toni.mlin.Core;
using com.toni.mlin.Match.Board;
using com.toni.mlin.Match.Player;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.toni.mlin.Match.Piece
{
    public class PieceView : MonoBehaviour, IPointerDownHandler, IObserver
    {
        [SerializeField] private Image image;
        [SerializeField] private Image outline;
        [SerializeField] private Transform BoardPieceContainer;
        [SerializeField] private AudioClip selectAudioClip;
        [SerializeField] private AudioClip endMoveAudioClip;
        [SerializeField] private AudioClip removeAudioClip;

        [CanBeNull]
        private Tweener removeIndicatorTween;

        public bool InHand => CurrentNode == null;

        public PlayerId Owner { get; private set; }

        public Node CurrentNode { get; private set; }

        public bool Removed { get; private set; }

        public void Setup(Player.Player player)
        {
            Owner = player.PlayerId;
            this.image.color = player.Color;
            Debug.Log($"SETUP {player.PlayerId} {player.Color}");
        }

        public void PlaceOnNode(NodeView targetNode)
        {
            if (CurrentNode != null)
            {
                if (!BoardController.IsNeighbor(CurrentNode, targetNode.Model) && MatchController.Instance.MatchState == MatchState.Moving) return;
                CurrentNode.Occupy(PlayerId.None);
            }

            transform.SetParent(this.BoardPieceContainer);
            transform.DOLocalMove(targetNode.transform.localPosition, 1f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                SoundController.Instance.PlaySound(this.endMoveAudioClip);
            });
            CurrentNode = targetNode.Model;
            this.Deselect();
            BoardController.Instance.CheckMill(Owner);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (MatchController.Instance.MatchState == MatchState.Removing)
            {
                if (MatchController.Instance.CurrentPlayerId == Owner) return;
                if (InHand) return;
                this.RemovePiece();
                SoundController.Instance.PlaySound(this.removeAudioClip);
                return;
            }

            if (MatchController.Instance.CurrentPlayerId != Owner) return;

            if (MatchController.Instance.MatchState == MatchState.Placing)
            {
                if (!InHand) return;
                this.Select();
            }

            if (MatchController.Instance.MatchState is MatchState.Moving or MatchState.Flying)
            {
                this.Select();
            }
        }

        public void Select()
        {
            this.outline.gameObject.SetActive(true);
            PlayerController.Instance.SelectPiece(this);
            SoundController.Instance.PlaySound(this.selectAudioClip);
        }

        public void Deselect()
        {
            this.outline.gameObject.SetActive(false);
            PlayerController.Instance.DeselectPiece();
        }

        private void RemovePiece()
        {
            CurrentNode.Occupy(PlayerId.None);
            Removed = true;
            transform.DOScale(Vector3.zero, 1).SetEase(Ease.InQuart).OnComplete(() =>
            {
                Destroy(gameObject);
            });

            PieceController.Instance.Unregister(this);

            MatchController.Instance.PieceRemoved();
        }

        [ObserverMethod]
        private void OnNextPlayerTurn()
        {
            if (Owner == MatchController.Instance.CurrentPlayerId)
            {
                this.outline.gameObject.SetActive(true);
            }
        }

        [ObserverMethod]
        private void OnRemovingMatchState()
        {
            if (MatchController.Instance.CurrentPlayerId == Owner || InHand) return;
            this.removeIndicatorTween = transform.DOPunchScale(Vector3.one * 0.1f, 1f, 0, 0).SetLoops(-1);
            this.removeIndicatorTween.Play();
        }

        [ObserverMethod]
        private void OnPieceRemoved()
        {
            this.removeIndicatorTween?.Kill();
            transform.localScale = Vector3.one;
        }

        [ObserverMethod]
        private void OnPieceSelected(PieceView selectedPiece)
        {
            if (MatchController.Instance.CurrentPlayerId == Owner && selectedPiece != this)
            {
                this.outline.gameObject.SetActive(false);
            }
        }

        private void Awake()
        {
            MatchController.Instance.Attach(this);
            PlayerController.Instance.Attach(this);
        }
    }
}