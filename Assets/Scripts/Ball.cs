using System.Collections;
using MiniIT.ARKANOID.Settings;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D            rigidbody2D;
        [SerializeField] private PlatformController     platformController;
        
        private bool                                    ballIsActive;
        private bool                                    respawned;
        private Vector3                                 ballPosition;
        
        private void Start () 
        {
            ballPosition = platformController.GetBallStartPosition();
        }
        
        public void Update () 
        {
            if (respawned)
            {
                transform.position = platformController.GetBallStartPosition();
            }
            
            if (Input.touches.Length > 0) 
            {
                if (!ballIsActive)
                {
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
            }

            if (collision.gameObject.CompareTag("BottomWall"))
            {
                StartCoroutine(RespawnBall());
            }
        }

        private IEnumerator RespawnBall()
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            transform.position = platformController.GetBallStartPosition();
            respawned = true;
            
            yield return new WaitForSeconds(SettingsProvider.Get<BallSettings>().BallRespawnTime);
            
            respawned = false;
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            rigidbody2D.AddForce(SettingsProvider.Get<BallSettings>().StartBallVector);
        }
    }
}
