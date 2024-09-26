using com.toni.mlin.Core;
using com.toni.mlin.Match;
using com.toni.mlin.Match.Player;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.toni.mlin.MainMenu.Settings
{
    public class SettingsView : UIView
    {
        [SerializeField] private GameObject popUp;
        [SerializeField] private Slider gridSizeSlider;
        [SerializeField] private TMP_Text gridSizeText;

        [SerializeField] private TMP_InputField player1NameInput;
        [SerializeField] private TMP_InputField player2NameInput;

        public override void Show()
        {
            base.Show();
            this.popUp.transform.localScale = Vector3.zero;
            this.popUp.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InCubic);

            this.gridSizeSlider.value = MatchController.Instance.MatchConfig.Size;
            this.player1NameInput.SetTextWithoutNotify(PlayerController.Instance.Player1.Name);
            this.player2NameInput.SetTextWithoutNotify(PlayerController.Instance.Player2.Name);
        }

        public void OnGridSliderValueChanged(float value)
        {
            this.gridSizeText.SetText(Mathf.RoundToInt(value).ToString());
            MatchController.Instance.MatchConfig.SetSize(Mathf.RoundToInt(value));
        }

        public void Player1EndEdit(string value)
        {
            if (value == PlayerController.Instance.Player2.Name)
            {
                this.player1NameInput.SetTextWithoutNotify(PlayerController.Instance.Player1.Name);
                return;
            }

            PlayerController.Instance.Player1.SetName(value);
        }

        public void Player2EndEdit(string value)
        {
            if (value == PlayerController.Instance.Player1.Name)
            {
                this.player2NameInput.SetTextWithoutNotify(PlayerController.Instance.Player2.Name);
                return;
            }

            PlayerController.Instance.Player2.SetName(value);
        }
    }
}