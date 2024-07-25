using Fusion;
using MiniIT.ARKANOID.Settings;
using TMPro;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class NetworkedBrick : NetworkBehaviour
    {
        [SerializeField] private TMP_Text       healthTMP; 
        
        private int                             maxHealth;
        [Networked] private int                 currentHealth { get; set; }
        private BaseGameUIController            gameUIController;
        private BaseGameField                   gameField;
        [Networked] private int                 lastDamageOwner { get; set; }
        
        [Inject]
        public void Construct(BaseGameUIController gameUIController, BaseGameField gameField)
        {
            this.gameUIController = gameUIController;
            this.gameField = gameField;
        }
        
        private void Start()
        {
            if(Runner.IsServer)
            {
                maxHealth = SettingsProvider.Get<GameFieldSettings>().GetRandomHealth;
                currentHealth = maxHealth;
                RpcSetHealth(-1, currentHealth);
            }
        }

        private void HealthChanged()
        {
            healthTMP.text = currentHealth.ToString();

            if (currentHealth <= 0)
            {
                gameUIController.ShowScore(lastDamageOwner, maxHealth);
                gameField.BrickDestroyed(this);
                Destroy(gameObject);
            }
        }

        [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
        private void RpcSetHealth(int ballOwnerId, int newHealth)
        {
            currentHealth = newHealth;
            lastDamageOwner = ballOwnerId;
            HealthChanged();
        }

        public void GetDamage(int ballOwnerId)
        {
            currentHealth--;
            RpcSetHealth(ballOwnerId, currentHealth);
        }
    }
}