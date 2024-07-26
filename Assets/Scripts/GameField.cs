using System;
using System.Collections.Generic;
using MiniIT.ARKANOID.Settings;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Random = UnityEngine.Random;

namespace MiniIT.ARKANOID
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Transform          parentTransform;
            
        private int[,]                              bricks;
            
        private GameFieldSettings                   GameFieldSettings => SettingsProvider.Get<GameFieldSettings>();
    
        private GameController                      gameController;
        private SoundController                     soundController;
        private GameUIController                    gameUIController;
        private ObjectPool<Brick>                   bricksPool;
        
        [Inject]
        private void Construct(GameController gameController, SoundController soundController, GameUIController gameUIController)
        {
            this.gameController = gameController;
            this.soundController = soundController;
            this.gameUIController = gameUIController;
        }
        
        private void Start()
        {
            bricksPool = new ObjectPool<Brick>(
                () =>
                {
                    Brick brick = Instantiate(GameFieldSettings.BrickPrefab, parentTransform);
                    return brick;
                },
                button => {button.gameObject.SetActive(true);},
                button => {button.gameObject.SetActive(false);},
                button => {Destroy(button.gameObject);});
            
            soundController.PlayMusic(MusicType.InGame);
            switch (gameController.GameType)
            {
                case GameType.Full:
                    CreateBricksWithSpacingAroundTheParent();
                    break;
                case GameType.Random:
                    CreateRandomBricks();
                    CreateFieldFromRandomBricks();
                    break;
            }
        }

        public void BrickDestroyed(Brick brick)
        {
            bricksPool.Release(brick);

            if (bricksPool.CountActive == 0)
            {
                gameUIController.ShowWinPopup();
            }
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
                        
                        bricksPool.Get().transform.SetPositionAndRotation(position, Quaternion.identity);
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
                    
                    bricksPool.Get().transform.SetPositionAndRotation(position, Quaternion.identity);
                }
            }
        }
    }
}
