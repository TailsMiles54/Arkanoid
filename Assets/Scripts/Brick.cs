using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniIT.ARCANOID
{
    public class Brick : MonoBehaviour
    {
        private int                             maxHealth;
        private int                             currentHealth;
        [SerializeField] private TMP_Text       healthTMP; 

        private void Start()
        {
            maxHealth = Random.Range(1, 11);
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