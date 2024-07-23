using Fusion;
using MiniIT.ARKANOID.Settings;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class MultiplayerPlatformController : NetworkBehaviour
    {
        private Vector3                             playerPosition;
        [SerializeField] private Transform          ballStartPosition;
     
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
                //data.direction.Normalize();
                
                transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(data.direction.x / Screen.width * 4 - 2,
                    playerPosition.y), SettingsProvider.Get<PlayerSettings>().PlayerSpeed * Time.deltaTime);
                
                //_cc.Move(5*data.direction*Runner.DeltaTime);
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