using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniIT.ARKANOID
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text            scoreTMP;

        private int                                  currentScore;

        public void AddScore(int value)
        {
            currentScore += value;
            scoreTMP.text = $"Score: {currentScore}";
        }

        public void Exit()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}