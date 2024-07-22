using UnityEngine;

namespace MiniIT.ARKANOID.Settings
{
    [CreateAssetMenu(menuName = "Arkanoid/GameFieldSettings", fileName = "GameFieldSettings", order = 0)]
    public class GameFieldSettings : ScriptableObject
    {
        [SerializeField] private int                brickHealthRangeMin;
        [SerializeField] private int                brickHealthRangeMax;
        [SerializeField] private GameObject         brickPrefab;
        [SerializeField] private int                rows;
        [SerializeField] private int                columns;
        [SerializeField] private float              spacing;
        
        public int                                  GetRandomHealth => Random.Range(brickHealthRangeMin, brickHealthRangeMax + 1);
        public GameObject                           BrickPrefab => brickPrefab;
        public int                                  Rows => rows;
        public int                                  Columns => columns;
        public float                                Spacing => spacing;
    }
}