using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    public class NodeView : MonoBehaviour
    {
        private int offset = 64;

        public Node Model { get; private set; }

        public RectTransform RectTransform { get; private set; }

        public void Setup(Node nodeModel)
        {
            Model = nodeModel;
            var x = Model.Coordinates.x;
            var y = Model.Coordinates.y;
            var depth = Model.Depth;

            RectTransform.anchoredPosition = new Vector2(-this.offset * (depth + 1) + x * this.offset * (depth + 1), -this.offset * (depth + 1) + y * this.offset * (depth + 1));
        }

        private void Awake()
        {
            RectTransform = this.GetComponent<RectTransform>();
        }
    }
}