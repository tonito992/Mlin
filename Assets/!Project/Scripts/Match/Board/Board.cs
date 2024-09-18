using System;
using System.Collections.Generic;

namespace com.toni.mlin.Match.Board
{
    [Serializable]
    public class Board
    {
        public List<Node> Nodes { get; }
        public List<Line> Lines { get; }

        public Board()
        {
            Nodes = new List<Node>();
            Lines = new List<Line>();
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

        public void AddLine(Line line)
        {
            Lines.Add(line);
        }
    }
}