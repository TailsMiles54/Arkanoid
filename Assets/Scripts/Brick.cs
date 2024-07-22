using MiniIT.ARKANOID.Settings;
using TMPro;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private TMP_Text       healthTMP; 
        
        private int                             maxHealth;
        private int                             currentHealth;
        private ScoreController                 scoreController;
        private GameField                       gameField;
        
        [Inject]
        public void Construct(ScoreController injectScoreController, GameField injectGameField)
        {
            scoreController = injectScoreController;
            gameField = injectGameField;
        }
        
        private void Start()
        {
            maxHealth = SettingsProvider.Get<GameFieldSettings>().GetRandomHealth;
            currentHealth = maxHealth;
            healthTMP.text = currentHealth.ToString();
        }

        public void GetDamage()
        {
            currentHealth--;
            healthTMP.text = currentHealth.ToString();

            if (currentHealth <= 0)
            {
                scoreController.AddScore(maxHealth);
                gameField.BrickDestroyed(this);
                Destroy(gameObject);
            }
        }
    }
}