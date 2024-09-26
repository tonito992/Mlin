using System;
using com.toni.mlin.Match.Player;
using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    [Serializable]
    public class Node
    {
        public Vector2Int Coordinates { get; private set; }

        public int Depth { get; private set; }

        public PlayerId Occupant { get; private set; }

        public static Node Create(int depth, int x, int y)
        {
            return new Node
            {
                Depth = depth,
                Coordinates = new Vector2Int(x, y),
                Occupant = PlayerId.None
            };
        }

        public void Occupy(PlayerId playerId)
        {
            Occupant = playerId;
        }
    }
}