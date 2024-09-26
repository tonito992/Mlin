using System;
using System.Linq;
using com.toni.mlin.Core;
using com.toni.mlin.Match.Player;
using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    public class BoardController : MonoController<BoardController>, IObserver
    {
        private Board board;

        public Board GenerateBoard(int depth)
        {
            this.board = new Board(depth);

            for (var i = 0; i < depth; i++)
            {
                AddNodes(i, ref this.board);
                AddLines(i, ref this.board);
            }

            return this.board;
        }

        public void CheckMill(PlayerId playerId)
        {
            if (this.board.IsMillFormed(playerId))
            {
                MatchController.Instance.MillFound();
            }
            else
            {
                MatchController.Instance.PiecePlaced();
            }
        }

        public static bool IsNeighbor(Node node1, Node node2)
        {
            var isSameDepthAdjacent =
                node1.Depth == node2.Depth &&
                ((node1.Coordinates.y == node2.Coordinates.y && Math.Abs(node1.Coordinates.x - node2.Coordinates.x) == 1) ||
                 (node1.Coordinates.x == node2.Coordinates.x && Math.Abs(node1.Coordinates.y - node2.Coordinates.y) == 1));

            var isDifferentDepthConnected =
                node1.Coordinates.x == node2.Coordinates.x && node1.Coordinates.y == node2.Coordinates.y &&
                Math.Abs(node1.Depth - node2.Depth) == 1 && IsMiddleNode(node1) && IsMiddleNode(node2);

            return isSameDepthAdjacent || isDifferentDepthConnected;
        }

        private static bool IsMiddleNode(Node node)
        {
            return (node.Coordinates.x == 1 && node.Coordinates.y != 1) ||
                   (node.Coordinates.y == 1 && node.Coordinates.x != 1);
        }

        private static void AddNodes(int depth, ref Board board)
        {
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    if (IsCenter(x, y)) continue;

                    var node = Node.Create(depth, x, y);
                    node.Occupy(PlayerId.None);
                    board.AddNode(node);

                    if (depth > 0 && !IsCorner(x, y))
                    {
                        var prevNode = board.Nodes.First(n => n.Coordinates == node.Coordinates && n.Depth == depth - 1);
                        board.AddLine(Line.Create(depth, node, prevNode));
                    }
                }
            }
        }

        private static bool IsCenter(int x, int y) => x == 1 && y == 1;

        private static bool IsCorner(int x, int y) => x is 0 or 2 && y is 0 or 2;

        private static void AddLines(int depth, ref Board board)
        {
            var nodesAtDepth = board.Nodes.Where(node => node.Depth == depth).ToList();
            var directions = new Vector2Int[]
            {
                new(-1, 0), // Left
                new(1, 0), // Right
                new(0, -1), // Down
                new(0, 1) // Up
            };

            foreach (var node in nodesAtDepth)
            {
                foreach (var direction in directions)
                {
                    var neighbor = nodesAtDepth.FirstOrDefault(n => n.Coordinates == node.Coordinates + direction);
                    if (neighbor != null)
                    {
                        board.AddLine(Line.Create(node.Depth, node, neighbor));
                    }
                }
            }
        }

        [ObserverMethod]
        private void OnNextPlayerTurn()
        {
            this.board.RemoveInvalidMills();
        }

        private void Awake()
        {
            MatchController.Instance.Attach(this);
        }
    }
}