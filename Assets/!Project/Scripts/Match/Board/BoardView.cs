using System.Collections.Generic;
using System.Linq;
using com.toni.mlin.Core;
using com.toni.mlin.Ui;
using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    public class BoardView : UIView
    {
        [SerializeField] private NodeView nodeView;
        [SerializeField] private UILineRenderer lineView;
        [SerializeField] private Transform nodesParent;
        [SerializeField] private Transform linesParent;

        private List<NodeView> nodeViews = new();
        private List<UILineRenderer> lines = new();
        private Board boardModel;

        public void Setup()
        {
            this.Clear();

            this.boardModel = BoardController.Instance.GenerateBoard(MatchController.Instance.MatchConfig.Size);

            foreach (var boardNode in this.boardModel.Nodes)
            {
                var newNode = Instantiate(this.nodeView, this.nodesParent);
                newNode.gameObject.SetActive(true);
                newNode.Setup(boardNode);
                this.nodeViews.Add(newNode);
            }

            foreach (var line in this.boardModel.Lines)
            {
                var newLine = Instantiate(this.lineView, this.linesParent);
                newLine.gameObject.SetActive(true);
                var startNode = this.nodeViews.First(node => node.Model == line.Start);
                var endNode = this.nodeViews.First(node => node.Model == line.End);
                newLine.points[0] = startNode.RectTransform.anchoredPosition;
                newLine.points[1] = endNode.RectTransform.anchoredPosition;
                this.lines.Add(newLine);
            }
        }

        private void Clear()
        {
            foreach (var node in this.nodeViews)
            {
                Destroy(node.gameObject);
            }

            this.nodeViews.Clear();

            foreach (var line in this.lines)
            {
                Destroy(line.gameObject);
            }

            this.lines.Clear();
        }
    }
}