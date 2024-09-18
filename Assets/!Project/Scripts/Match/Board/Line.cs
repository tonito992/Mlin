using System;
using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    [Serializable]
    public class Line
    {
        public int Depth { get; private set; }
        public Node Start { get; private set; }
        public Node End { get; private set; }

        public static Line Create(int depth, Node start, Node end)
        {
            return new Line
            {
                Depth = depth,
                Start = start,
                End = end
            };
        }
    }
}