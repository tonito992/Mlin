using System;
using UnityEngine;

namespace com.toni.mlin.Match.Player
{
    [Serializable]
    public class Player
    {
        [SerializeField] private PlayerId playerId;
        [SerializeField] private string name;
        [SerializeField] private Color color;

        public PlayerId PlayerId => this.playerId;
        public string Name => this.name;
        public Color Color => this.color;

        public Player(PlayerId playerId, string name, Color color)
        {
            this.playerId = playerId;
            this.name = name;
            this.color = color;
        }

        public void SetColor(Color value)
        {
            this.color = value;
            Debug.Log($"Set color {this.playerId} {value}");
        }

        public void SetName(string value)
        {
            this.name = value;
        }
    }
}