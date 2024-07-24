using System.Collections.Generic;
using System.Linq;
using Fusion;
using MiniIT.ARKANOID.Settings;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class MultiplayerGameField : BaseGameField
    {
        [SerializeField] private Transform      parentTransform;
        [SerializeField] private Vector3        player1SpawnPosition;
        [SerializeField] private Vector3        player2SpawnPosition;
        
        private int[,]                          bricks;
        private List<Brick>                     bricksOnField;
        private GameController                  gameController;
        private PhotonService                   photonService;
        
        private GameFieldSettings               GameFieldSettings => SettingsProvider.Get<GameFieldSettings>();
        
        [Inject]
        private void Construct(GameController gameController, PhotonService photonService)
        {
            this.gameController = gameController;
            this.photonService = photonService;
        }
        
        private void Start()
        {
            bricksOnField = new List<Brick>();
            
            if(!photonService.PhotonController.Runner.IsServer)
                return;
            
            switch (gameController.GameType)
            {
                case GameType.Full:
                    CreateBricksWithSpacingAroundTheParent();
                    break;
                case GameType.Random:
                    CreateRandomBricks();
                    CreateFieldFromRandomBricks();
                    break;
                case GameType.Preset:
                    //TODO: generate with preset
                    break;
            }

            photonService.PhotonController.SpawnPlayers(player1SpawnPosition, player2SpawnPosition);
        }

        public override void BrickDestroyed(Brick brick)
        {
            bricksOnField.Remove(brick);
            
            //TODO: check bricks count
        }
        
        /// <summary>
        /// Create game field for array with random bricks positions
        /// </summary>
        private async void CreateFieldFromRandomBricks()
        {
            for (int i = 0; i < GameFieldSettings.Rows; i++)
            {
                for (int j = 0; j < GameFieldSettings.Columns; j++)
                {
                    if (bricks[i, j] > 0)
                    {
                        var brickLocalScale = GameFieldSettings.BrickPrefab.transform.localScale;
                        Vector3 position = new Vector3(
                            j * (brickLocalScale.x + GameFieldSettings.Spacing) - GameFieldSettings.Columns * (brickLocalScale.x + GameFieldSettings.Spacing) / 2.0f + 0.15f,
                            i * (brickLocalScale.y + GameFieldSettings.Spacing) - GameFieldSettings.Rows * (brickLocalScale.y + GameFieldSettings.Spacing) / 2.0f,
                            0);

                        var runner = photonService.PhotonController.Runner;
                        
                        var obj = await runner.SpawnAsync(GameFieldSettings.NetworkBrickPrefab,
                            position, Quaternion.identity, runner.ActivePlayers.First(), (runner, o) =>
                            {
                                bricksOnField.Add(o.GetComponent<Brick>());
                                o.gameObject.transform.SetParent(parentTransform);
                            });
                        
                        // var brick = Instantiate(GameFieldSettings.BrickPrefab, position, Quaternion.identity, parentTransform);
                        // bricksOnField.Add(brick);
                    }
                }
            }
        }

        /// <summary>
        /// Generate array filled random bricks positions
        /// </summary>
        private void CreateRandomBricks()
        {
            bricks = new int[GameFieldSettings.Rows, GameFieldSettings.Columns];
            
            for (int i = 0; i < GameFieldSettings.Rows; i++)
            {
                for (int j = 0; j < GameFieldSettings.Columns; j++)
                {
                    int hasBrick = Random.Range(0, 3);
                    bricks[i, j] = hasBrick;
                }
            }
        }

        /// <summary>
        /// Create full game field
        /// </summary>
        private async void CreateBricksWithSpacingAroundTheParent()
        {
            var brickLocalScale = GameFieldSettings.BrickPrefab.transform.localScale;
            float halfWidth = GameFieldSettings.Columns * (brickLocalScale.x + GameFieldSettings.Spacing) / 2.0f;
            float halfHeight = GameFieldSettings.Rows * (brickLocalScale.y + GameFieldSettings.Spacing) / 2.0f;
            
            for (int i = 0; i < GameFieldSettings.Rows; i++)
            {
                for (int j = 0; j < GameFieldSettings.Columns; j++)
                {
                    Vector3 position = new Vector3(
                        j * (brickLocalScale.x + GameFieldSettings.Spacing) - halfWidth + 0.15f,
                        i * (brickLocalScale.y + GameFieldSettings.Spacing) - halfHeight,
                        0);
                    
                    var runner = photonService.PhotonController.Runner;
                        
                    await runner.SpawnAsync(GameFieldSettings.NetworkBrickPrefab,
                        position, Quaternion.identity, runner.ActivePlayers.First(), (runner, o) =>
                        {
                            bricksOnField.Add(o.GetComponent<Brick>());
                            o.gameObject.transform.SetParent(parentTransform);
                        });

                    // Instantiate(GameFieldSettings.BrickPrefab, position, Quaternion.identity, parentTransform);
                }
            }
        }
    }
}