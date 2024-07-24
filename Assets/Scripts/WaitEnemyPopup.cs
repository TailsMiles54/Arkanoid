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

        private PhotonService                     photonService;

        [Inject]
        public void Construct(PhotonService photonService)
        {
            this.photonService = photonService;
        }

        public void ShowWaitEnemyPopup(GameType gameType)
        {
            canvasGroup.blocksRaycasts = true;
            contentTMP.text = $"Wait enemy for {gameType} game type";
            canvasGroup.DOFade(1f, 0.3f);
        }
        
        public void HideWaitEnemyPopup()
        {
            photonService.PhotonController.Runner.Shutdown();
            canvasGroup.DOFade(0f, 0.3f).OnComplete(() => 
            {
                canvasGroup.blocksRaycasts = false;
            });
        }
    }
}
