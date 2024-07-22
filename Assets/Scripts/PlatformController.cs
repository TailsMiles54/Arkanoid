using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.ARCANOID
{
    public class PlatformController : MonoBehaviour
    {
        public float                                playerVelocity;
        private Vector3                             playerPosition;
        private int                                 boundary = 2;
        [SerializeField] private Transform          ballStartPosition;
     
        
        void Start () 
        {
            playerPosition = gameObject.transform.position;
        }

        void Update ()
        {
            MoveToTouch();
        }

        private void MoveToTouch()
        {
            // Движение к позиции нажатия по x
            if (Input.touches.Length > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Input.touches[0].position.x / Screen.width * 4 - 2,
                    playerPosition.y), playerVelocity * Time.deltaTime);
            }
        }

        public Vector3 GetBallStartPosition()
        {
            return ballStartPosition.position;
        }
    }
}
