using TMPro;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text            scoreTMP;

        private int                                  currentScore;

        public void AddScore(int value)
        {
            currentScore += value;
            scoreTMP.text = $"Score: {currentScore}";
        }
    }
}