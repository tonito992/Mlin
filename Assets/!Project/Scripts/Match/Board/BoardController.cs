using System.Linq;
using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    public class BoardController : MonoBehaviour
    {
        public static Board GenerateBoard(int depth)
        {
            var board = new Board();

            for (var i = 0; i < depth; i++)
            {
                AddNodes(i, ref board);
                AddLines(i, ref board);
            }

            return board;
        }

        private static void AddNodes(int depth, ref Board board)
        {
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    if (IsCenter(x, y)) continue;

                    var node = Node.Create(depth, x, y);
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
                new(1, 0),  // Right
                new(0, -1), // Down
                new(0, 1)   // Up
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
    }
}
