using UnityEngine;

namespace com.toni.mlin.Match.Board
{
    public class NodeView : MonoBehaviour
    {
        public Node Model { get; private set; }

        public RectTransform RectTransform { get; private set; }

        public void Setup(Node nodeModel)
        {
            Model = nodeModel;
            var x = Model.Coordinates.x;
            var y = Model.Coordinates.y;
            var depth = Model.Depth;

            RectTransform.anchoredPosition = new Vector2(-64 * (depth + 1) + x * 64 * (depth + 1), -64 * (depth + 1) + y * 64 * (depth + 1));
        }

        private void Awake()
        {
            RectTransform = this.GetComponent<RectTransform>();
        }
    }
}