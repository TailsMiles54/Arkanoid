using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class WaitEnemyPopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup      canvasGroup;
        [SerializeField] private TMP_Text         contentTMP;

        private PhotonController                  photonController;

        [Inject]
        public void Construct(PhotonController photonController)
        {
            this.photonController = photonController;
        }

        public void ShowWaitEnemyPopup(GameType gameType)
        {
            canvasGroup.blocksRaycasts = true;
            contentTMP.text = $"Wait enemy for {gameType}";
            canvasGroup.DOFade(1f, 0.3f);
        }
        
        public void HideWaitEnemyPopup()
        {
            photonController.Runner.Shutdown();
            canvasGroup.DOFade(0f, 0.3f).OnComplete(() => 
            {
                canvasGroup.blocksRaycasts = false;
            });
        }
    }
}
