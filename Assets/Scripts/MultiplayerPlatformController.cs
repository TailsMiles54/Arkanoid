using Fusion;
using MiniIT.ARKANOID.Settings;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class MultiplayerPlatformController : NetworkBehaviour
    {
        [SerializeField] private Transform          ballStartPosition;
        private Vector3                             playerPosition;

        public Vector3                              BallStartPosition => ballStartPosition.position;
     
        void Start () 
        {
            playerPosition = gameObject.transform.position;
        }

        void Update ()
        {
            if (HasStateAuthority)
            {
                MoveToTouch();
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData data))
            {
                if(data.direction != Vector3.zero)
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                        new Vector2(data.direction.x / Screen.width * 4 - 2,
                            playerPosition.y), SettingsProvider.Get<PlayerSettings>().PlayerSpeed * Time.deltaTime);
                }
            }
        }

        private void MoveToTouch()
        {
            if (Input.touches.Length > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Input.touches[0].position.x / Screen.width * 4 - 2,
                    playerPosition.y), SettingsProvider.Get<PlayerSettings>().PlayerSpeed * Time.deltaTime);
            }
            else if (Input.GetMouseButton(0))
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Input.mousePosition.x / Screen.width * 4 - 2,
                    playerPosition.y), SettingsProvider.Get<PlayerSettings>().PlayerSpeed * Time.deltaTime);
            }
        }

        public Vector3 GetBallStartPosition()
        {
            return ballStartPosition.position;
        }
    }
}