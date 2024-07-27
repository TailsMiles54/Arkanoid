using MiniIT.ARKANOID.Settings;
using UnityEngine;

namespace MiniIT.ARKANOID.Controllers
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private Transform          ballStartPosition;
        
        private Vector3                             playerPosition;
     
        void Start() 
        {
            playerPosition = gameObject.transform.position;
        }

        void Update()
        {
            MoveToTouch();
        }

        private void MoveToTouch()
        {
            if (Input.touches.Length > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Input.touches[0].position.x / Screen.width * 4 - 2,
                    playerPosition.y), SettingsProvider.Get<PlayerSettings>().PlayerSpeed * Time.deltaTime);
            }
        }

        public Vector3 GetBallStartPosition()
        {
            return ballStartPosition.position;
        }
    }
}