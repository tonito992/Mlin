using System.Collections.Generic;
using System.Linq;
using com.toni.mlin.Ui;
using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private NodeView nodeView;
        [SerializeField] private UILineRenderer lineView;

        private List<NodeView> nodeViews;
        [SerializeField] private Board boardModel;

        private void Awake()
        {
            this.boardModel = BoardController.GenerateBoard(4);
            this.nodeViews = new List<NodeView>();

            foreach (var boardNode in this.boardModel.Nodes)
            {
                var newNode = Instantiate(this.nodeView, transform);
                newNode.gameObject.SetActive(true);
                newNode.Setup(boardNode);
                this.nodeViews.Add(newNode);
            }

            foreach (var line in this.boardModel.Lines)
            {
                var newLine = Instantiate(this.lineView, transform);
                newLine.gameObject.SetActive(true);
                var startNode = this.nodeViews.First(node => node.Model == line.Start);
                var endNode = this.nodeViews.First(node => node.Model == line.End);
                newLine.points[0] = startNode.RectTransform.anchoredPosition;
                newLine.points[1] = endNode.RectTransform.anchoredPosition;
            }
        }
    }
}