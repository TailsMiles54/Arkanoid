using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniIT.ARKANOID
{
    public class MultiplayerGameUIController : BaseGameUIController
    {
        [SerializeField] private TMP_Text            yourScore;
        [SerializeField] private TMP_Text            enemyScore;

        private int                                  yourcurrentScore;
        private int                                  enemycurrentScore;

        public override void AddScore(int value)
        {
            yourcurrentScore += value;
            yourScore.text = $"Score: {yourcurrentScore}";
            
            enemycurrentScore += value;
            enemyScore.text = $"Score: {enemycurrentScore}";
        }
    }
}