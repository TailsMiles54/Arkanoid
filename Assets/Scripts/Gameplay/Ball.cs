using MiniIT.ARKANOID.Controllers;
using MiniIT.ARKANOID.Enums;
using MiniIT.ARKANOID.Settings;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID.Gameplay
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D            rigidbody2D;
        [SerializeField] private PlatformController     platformController;
        
        private bool                                    ballIsActive = false;
        private bool                                    respawned = false;
        private Vector3                                 ballPosition;
        private SoundController                         soundController;
        private GameUIController                        gameUIController;

        [Inject]
        public void Construct(SoundController soundController, GameUIController gameUIController)
        {
            this.soundController = soundController;
            this.gameUIController = gameUIController;
        }
        
        private void Start() 
        {
            ballPosition = platformController.GetBallStartPosition();
        }
        
        public void Update() 
        {
            if (respawned)
            {
                transform.position = platformController.GetBallStartPosition();
            }
            
            if (Input.touches.Length > 0) 
            {
                if (!ballIsActive)
                {
                    soundController.PlaySoundEffect(SoundType.BallStart);
                    rigidbody2D.AddForce(SettingsProvider.Get<BallSettings>().StartBallVector);
                    ballIsActive = !ballIsActive;
                }
		
                if (!ballIsActive && platformController != null)
                {
                    ballPosition.x = platformController.transform.position.x;
             
                    transform.position = ballPosition;
                }
            } 
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log($"Ball Collision {collision.gameObject.name}");
            if (collision.gameObject.CompareTag("Brick"))
            {
                var brick = collision.gameObject.GetComponent<Brick>();
                brick.GetDamage();
                soundController.PlaySoundEffect(SoundType.BrickHit);
            }

            if (collision.gameObject.CompareTag("BottomWall"))
            {
                gameUIController.ShowLosePopup();
                Destroy(gameObject);
            }
        }
    }
}
