using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class MultiplayerGameUIController : BaseGameUIController
    {
        [SerializeField] private TMP_Text            yourScore;
        [SerializeField] private TMP_Text            enemyScore;

        private int                                  yourcurrentScore;
        private int                                  enemycurrentScore;
        private PhotonService                        photonService;
        
        [Inject]
        public void Construct(PhotonService photonService)
        {
            this.photonService = photonService;
            photonService.PhotonController.PlayerLeft += PhotonControllerOnPlayerLeft;
        }

        private void PhotonControllerOnPlayerLeft()
        {
            photonService.PhotonController.Runner.Shutdown();
            SceneManager.LoadScene("MenuScene");
        }

        public override void AddScore(int value)
        {
            yourcurrentScore += value;
            yourScore.text = $"Score: {yourcurrentScore}";
            
            enemycurrentScore += value;
            enemyScore.text = $"Score: {enemycurrentScore}";
        }

        public override void Exit()
        {
            photonService.PhotonController.Runner.Shutdown();
            SceneManager.LoadScene("MenuScene");
        }

        ~MultiplayerGameUIController()
        {
            photonService.PhotonController.PlayerLeft -= PhotonControllerOnPlayerLeft;
        }
    }
}