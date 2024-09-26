using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    [Serializable]
    public class Board
    {
        [SerializeField] private List<Node> nodes;
        public List<Node> Nodes => this.nodes;
        public List<Line> Lines { get; }
        public int Depth { get; }
        public List<Mill> mills { get; }

        public Board(int depth)
        {
            this.nodes = new List<Node>();
            Lines = new List<Line>();
            mills = new List<Mill>();
            Depth = depth;
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

        public void AddLine(Line line)
        {
            Lines.Add(line);
        }

        public bool IsMillFormed(PlayerId playerId)
        {
            var groupedNodesByDepth = Nodes.GroupBy(n => n.Depth);

            foreach (var depthGroup in groupedNodesByDepth)
            {
                if (this.CheckConsecutiveNodes(depthGroup.ToList(), playerId, true))
                    return true;

                if (this.CheckConsecutiveNodes(depthGroup.ToList(), playerId, false))
                    return true;
            }

            if (this.CheckMillAcrossDepths(playerId))
                return true;

            return false;
        }

        private bool CheckConsecutiveNodes(List<Node> nodes, PlayerId playerId, bool checkHorizontal)
        {
            var groupedNodes = checkHorizontal
                ? nodes.GroupBy(n => n.Coordinates.y)
                : nodes.GroupBy(n => n.Coordinates.x);

            foreach (var group in groupedNodes)
            {
                var orderedNodes = checkHorizontal
                    ? group.OrderBy(n => n.Coordinates.x).ToList()
                    : group.OrderBy(n => n.Coordinates.y).ToList();

                if (orderedNodes.Count >= 3)
                {
                    for (int i = 0; i <= orderedNodes.Count - 3; i++)
                    {
                        if (orderedNodes[i].Occupant == playerId &&
                            orderedNodes[i + 1].Occupant == playerId &&
                            orderedNodes[i + 2].Occupant == playerId)
                        {
                            var mill = new Mill(orderedNodes);
                            if (mills.Contains(mill)) continue;
                            mills.Add(new Mill(orderedNodes));
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckMillAcrossDepths(PlayerId playerId)
        {
            var middleNodes = Nodes.Where(this.IsMiddleNode).ToList();

            var groupedByPosition = middleNodes.GroupBy(n => new { n.Coordinates.x, n.Coordinates.y });

            foreach (var group in groupedByPosition)
            {
                var orderedByDepth = group.OrderBy(n => n.Depth).ToList();

                if (orderedByDepth.Count >= 3)
                {
                    for (int i = 0; i <= orderedByDepth.Count - 3; i++)
                    {
                        if (orderedByDepth[i].Occupant == playerId &&
                            orderedByDepth[i + 1].Occupant == playerId &&
                            orderedByDepth[i + 2].Occupant == playerId)
                        {
                            var mill = new Mill(orderedByDepth);
                            if (mills.Contains(mill)) continue;
                            mills.Add(new Mill(orderedByDepth));
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsMiddleNode(Node node)
        {
            return (node.Coordinates.x == 1 && node.Coordinates.y != 1) ||
                   (node.Coordinates.y == 1 && node.Coordinates.x != 1);
        }

        public void RemoveInvalidMills()
        {
            for (int i = mills.Count - 1; i >= 0; i--)
            {
                var mill = mills[i];
                if (!this.IsMillValid(mill))
                {
                    mills.RemoveAt(i);
                }
            }
        }

        private bool IsMillValid(Mill mill)
        {
            var player = mill.Nodes[0].Occupant;
            return mill.Nodes.All(node => node.Occupant == player);
        }
    }

    public class Mill
    {
        public List<Node> Nodes { get; }

        public Mill(List<Node> nodes)
        {
            Nodes = nodes.OrderBy(n => n.Coordinates.x).ThenBy(n => n.Coordinates.y).ToList();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            var otherMill = (Mill)obj;
            return Nodes.SequenceEqual(otherMill.Nodes);
        }

        public override int GetHashCode()
        {
            int hash = 7;
            foreach (var node in Nodes)
            {
                hash = hash * 13 + node.Coordinates.x.GetHashCode();
                hash = hash * 13 + node.Coordinates.y.GetHashCode();
                hash = hash * 13 + node.Depth.GetHashCode();
            }

            return hash;
        }
    }
}