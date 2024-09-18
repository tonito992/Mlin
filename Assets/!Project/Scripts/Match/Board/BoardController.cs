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
                for (var x = 0; x < 3; x++)
                {
                    for (var y = 0; y < 3; y++)
                    {
                        if (x == 1 && y == 1) continue; // Avoid creating center node
                        board.AddNode(Node.Create(i, x, y));
                    }
                }

                GenerateLines(i, ref board);
            }

            return board;
        }

        private static void GenerateLines(int depth, ref Board board)
        {
            var nodes = board.Nodes.Where(node => node.Depth == depth).ToList();

            var directions = new Vector2Int[]
            {
                new(-1, 0), // Left
                new(1, 0), // Right
                new(0, -1), // Down
                new(0, 1) // Up
            };

            foreach (var node in nodes)
            {
                foreach (var direction in directions)
                {
                    var neighborCoordinates = node.Coordinates + direction;
                    var neighbor = nodes.FirstOrDefault(n => n.Coordinates == neighborCoordinates);

                    if (neighbor != null)
                    {
                        board.AddLine(Line.Create(node.Depth, node.Coordinates, neighbor.Coordinates));
                    }
                }
            }
        }
    }
}