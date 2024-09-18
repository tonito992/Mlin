using System;
using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    [Serializable]
    public class Node
    {
        public Vector2Int Coordinates { get; private set; }

        public int Depth { get; private set; }

        public static Node Create(int depth, int x, int y)
        {
            return new Node
            {
                Depth = depth,
                Coordinates = new Vector2Int(x, y)
            };
        }
    }
}