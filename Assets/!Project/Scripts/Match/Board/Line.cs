using System;
using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    [Serializable]
    public class Line
    {
        public int Depth { get; private set; }
        public Vector2Int Start { get; private set; }
        public Vector2Int End { get; private set; }

        public static Line Create(int depth, Vector2Int start, Vector2Int end)
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