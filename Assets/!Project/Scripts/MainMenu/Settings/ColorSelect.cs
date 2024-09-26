using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.toni.mlin.MainMenu.Settings
{
    public class ColorSelect : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private PlayerColorPicker playerColorPicker;
        [SerializeField] private Image image;
        [SerializeField] private GameObject selectedIndicator;
        [SerializeField] private Color color;

        public Color Color => this.color;

        public void OnPointerDown(PointerEventData eventData)
        {
            this.playerColorPicker.SelectColor(this.Color);
        }

        private void Awake()
        {
            this.image.color = this.Color;
        }

        public void Select()
        {
            this.selectedIndicator.SetActive(true);
        }

        public void Deselect()
        {
            this.selectedIndicator.SetActive(false);
        }
    }
}