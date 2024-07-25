﻿using MiniIT.ARKANOID.Settings;
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
        private BaseGameUIController            gameUIController;
        private BaseGameField                   gameField;
        
        [Inject]
        public void Construct(BaseGameUIController gameUIController, BaseGameField gameField)
        {
            this.gameUIController = gameUIController;
            this.gameField = gameField;
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
                gameUIController.ShowScore(maxHealth);
                gameField.BrickDestroyed(this);
                Destroy(gameObject);
            }
        }
    }
}