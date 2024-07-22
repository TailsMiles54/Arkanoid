using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.ARCANOID
{
    public class GameFieldCreator : MonoBehaviour
    {
        [SerializeField] private GameObject     brickPrefab;
        [SerializeField] private int            rows;
        [SerializeField] private int            columns;
        
        [SerializeField] private float          spacing;
        [SerializeField] private Transform      parentTransform;
        
        private int[,]                          bricks;
        
        private void Start()
        {
            //CreateBricksWithSpacingAroundTheParent();
            CreateRandomBricks();
            CreateFieldFromRandomBricks();
        }

        private void CreateFieldFromRandomBricks()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (bricks[i, j] > 0)
                    {
                        Vector3 position = new Vector3(
                            j * (brickPrefab.transform.localScale.x + spacing) - columns * (brickPrefab.transform.localScale.x + spacing) / 2.0f + 0.15f,
                            i * (brickPrefab.transform.localScale.y + spacing) - rows * (brickPrefab.transform.localScale.y + spacing) / 2.0f,
                            0);
                        
                        Instantiate(brickPrefab, position, Quaternion.identity, parentTransform);
                    }
                }
            }
        }

        private void CreateRandomBricks()
        {
            bricks = new int[rows, columns];
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int hasBrick = Random.Range(0, 3);
                    bricks[i, j] = hasBrick;
                }
            }
        }

        private void CreateBricksWithSpacingAroundTheParent()
        {
            float halfWidth = columns * (brickPrefab.transform.localScale.x + spacing) / 2.0f;
            float halfHeight = rows * (brickPrefab.transform.localScale.y + spacing) / 2.0f;
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector3 position = new Vector3(
                        j * (brickPrefab.transform.localScale.x + spacing) - halfWidth,
                        i * (brickPrefab.transform.localScale.y + spacing) - halfHeight,
                        0);
                    
                    Instantiate(brickPrefab, position, Quaternion.identity, parentTransform);
                }
            }
        }
    }
}
