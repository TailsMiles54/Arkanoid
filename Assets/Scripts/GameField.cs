using System.Collections.Generic;
using MiniIT.ARKANOID.Settings;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Transform      parentTransform;
        private int[,]                          bricks;
        private GameFieldSettings               GameFieldSettings => SettingsProvider.Get<GameFieldSettings>();

        private List<Brick>                     _bricksOnField;
        
        private void Start()
        {
            //CreateBricksWithSpacingAroundTheParent();
            CreateRandomBricks();
            CreateFieldFromRandomBricks();
        }

        public void BrickDestroyed(Brick brick)
        {
            _bricksOnField.Remove(brick);
            
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
                        _bricksOnField.Add(brick);
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
                        j * (brickLocalScale.x + GameFieldSettings.Spacing) - halfWidth,
                        i * (brickLocalScale.y + GameFieldSettings.Spacing) - halfHeight,
                        0);
                    
                    Instantiate(GameFieldSettings.BrickPrefab, position, Quaternion.identity, parentTransform);
                }
            }
        }
    }
}
