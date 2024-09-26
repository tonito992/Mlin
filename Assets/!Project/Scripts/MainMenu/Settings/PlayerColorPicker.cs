using System.Collections.Generic;
using com.toni.mlin.Match;
using UnityEngine;

namespace com.toni.mlin.MainMenu.Settings
{
    public class PlayerColorPicker : MonoBehaviour
    {
        [SerializeField] private PlayerId playerId;
        [SerializeField] private List<ColorSelect> colorSelects;

        public void SelectColor(Color color)
        {
            if (this.playerId == PlayerId.Player1)
            {
                if (PlayerController.Instance.Player2.Color == color) return;
                PlayerController.Instance.Player1.SetColor(color);
            }

            if (this.playerId == PlayerId.Player2)
            {
                if (PlayerController.Instance.Player1.Color == color) return;
                PlayerController.Instance.Player2.SetColor(color);

            }

            this.colorSelects.ForEach(colorSelect =>
            {
                if (colorSelect.Color == color)
                {
                    colorSelect.Select();
                }
                else
                {
                    colorSelect.Deselect();
                }
            });
        }
    }
}
