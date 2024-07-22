using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniIT.ARCANOID
{
    public class Ball : MonoBehaviour
    {
        private bool                                    ballIsActive;
        private bool                                    respawned;
        private Vector3                                 ballPosition;
        private Vector2                                 BallInitialForce => new Vector2(Random.Range(-50,50f),200f);
        [SerializeField] private Rigidbody2D            rigidbody2D;
        [SerializeField] private PlatformController     platformController;
        
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
                    rigidbody2D.AddForce(BallInitialForce);
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
                collision.gameObject.GetComponent<Brick>().GetDamage();
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
            
            yield return new WaitForSeconds(1.5f);
            
            respawned = false;
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            rigidbody2D.AddForce(BallInitialForce);
        }
    }
}
