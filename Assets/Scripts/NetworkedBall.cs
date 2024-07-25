using System.Collections;
using Fusion;
using MiniIT.ARKANOID.Settings;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class NetworkedBall : NetworkBehaviour
    {
        [SerializeField] private Rigidbody2D                        rigidbody2D;
        [SerializeField] private MultiplayerPlatformController      platformController;
        
        private bool                                                ballIsActive;
        private bool                                                respawned;
        private Vector3                                             ballPosition;
        private int                                                 playerOwnerId;

        public void Setup(MultiplayerPlatformController platformController, int playerOwnerId)
        {
            this.platformController = platformController;
            this.playerOwnerId = playerOwnerId;
            RpcSetBallOwner(playerOwnerId);
        }

        [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
        public void RpcSetBallOwner(int ballOwnerId)
        {
            playerOwnerId = ballOwnerId;
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData data))
            {
                Debug.Log("Button send1");
                if (data.buttons.IsSet(NetworkInputData.button))
                {
                    Debug.Log("Button send2");
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
        }

        public void Update() 
        {
            if (Runner.IsServer)
            {
                if (respawned)
                {
                    transform.position = platformController.GetBallStartPosition();
                }
            }
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if(Runner.IsServer)
            {
                if (collision.gameObject.CompareTag("Brick"))
                {
                    var brick = collision.gameObject.GetComponent<NetworkedBrick>();
                    brick.GetDamage(playerOwnerId);
                }

                if (collision.gameObject.CompareTag("BottomWall") || collision.gameObject.CompareTag("TopWall"))
                {
                    StartCoroutine(RespawnBall());
                }
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