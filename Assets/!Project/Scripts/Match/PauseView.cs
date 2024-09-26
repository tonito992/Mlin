using System;
using com.toni.mlin.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace com.toni.mlin.Match
{
    public class PauseView : UIView, IObserver
    {
        [SerializeField] private GameObject popUp;

        public override void Show()
        {
            base.Show();
            this.popUp.transform.localScale = Vector3.zero;
            this.popUp.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InCubic);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (this.container.gameObject.activeInHierarchy) return;
                this.Show();
            }
        }

        public void OnTapMainMenu()
        {
            this.Hide();
            MatchController.Instance.ExitGame();
        }

        public void OnTapRestart()
        {
            this.Hide();
            MatchController.Instance.ExitGame();
            MatchController.Instance.InitiateGame();
        }

        private void Awake()
        {
            MatchController.Instance.Attach(this);
        }
    }
}