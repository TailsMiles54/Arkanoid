using System.Collections.Generic;
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
        private PhotonController                photonController;
        
        private GameFieldSettings               GameFieldSettings => SettingsProvider.Get<GameFieldSettings>();
        
        [Inject]
        private void Construct(GameController gameController, PhotonController photonController)
        {
            this.gameController = gameController;
            this.photonController = photonController;
        }
        
        private void Start()
        {
            bricksOnField = new List<Brick>();
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

            photonController.SpawnPlayers(player1SpawnPosition, player2SpawnPosition);
        }

        public override void BrickDestroyed(Brick brick)
        {
            bricksOnField.Remove(brick);
            
            //TODO: check bricks count
        }
        
        /// <summary>
        /// Create game field for array with random bricks positions
        /// </summary>
        private void CreateFieldFromRandomBricks()
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
                        
                        var brick = Instantiate(GameFieldSettings.BrickPrefab, position, Quaternion.identity, parentTransform);
                        bricksOnField.Add(brick);
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
        private void CreateBricksWithSpacingAroundTheParent()
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
                    
                    Instantiate(GameFieldSettings.BrickPrefab, position, Quaternion.identity, parentTransform);
                }
            }
        }
    }
}