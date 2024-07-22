using MiniIT.ARKANOID.Settings;
using TMPro;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class Brick : MonoBehaviour
    {
        private int                             maxHealth;
        private int                             currentHealth;
        [SerializeField] private TMP_Text       healthTMP; 

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
                Destroy(gameObject);
            }
        }
    }
}