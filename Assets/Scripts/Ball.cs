using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.ARCANOID
{
    public class Ball : MonoBehaviour
    {
        private bool                                    ballIsActive;
        private Vector3                                 ballPosition;
        private Vector2                                 ballInitialForce;
        [SerializeField] private Rigidbody2D            rigidbody2D;
        [SerializeField] private GameObject             playerObject;
        
        private void Start () {
            // создаем силу
            ballInitialForce = new Vector2 (100.0f,300.0f);

            // переводим в неактивное состояние
            ballIsActive = false;

            // запоминаем положение
            ballPosition = transform.position;
        }
        
        public void Update () {
            
            if (Input.GetButtonUp("Fire1")) 
            {
                Debug.Log("Touched");
                
                if (!ballIsActive)
                {
                    rigidbody2D.AddForce(ballInitialForce);
                    ballIsActive = !ballIsActive;
                }
		
                if (!ballIsActive && playerObject != null)
                {
                    ballPosition.x = playerObject.transform.position.x;
             
                    transform.position = ballPosition;
                }
                
                if (ballIsActive && transform.position.y < -6) 
                {
                    ballIsActive = !ballIsActive;
                    ballPosition.x = playerObject.transform.position.x;
                    ballPosition.y = -4.2f;
                    transform.position = ballPosition;
 
                    rigidbody2D.isKinematic = true;
                }
            } 
        }
    }
}
