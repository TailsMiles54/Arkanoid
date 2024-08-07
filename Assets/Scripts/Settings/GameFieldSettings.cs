﻿using MiniIT.ARKANOID.Gameplay;
using UnityEngine;

namespace MiniIT.ARKANOID.Settings
{
    [CreateAssetMenu(menuName = "Arkanoid/GameFieldSettings", fileName = "GameFieldSettings", order = 0)]
    public class GameFieldSettings : ScriptableObject
    {
        [SerializeField] private int                brickHealthRangeMin;
        [SerializeField] private int                brickHealthRangeMax;
        [SerializeField] private Brick              brickPrefab;
        [SerializeField, Range(1,19)] private int   rows;
        [SerializeField, Range(1,19)] private int   columns;
        [SerializeField] private float              spacing;
        [SerializeField] private Sprite             winSprite;
        
        public int                                  GetRandomHealth => Random.Range(brickHealthRangeMin, brickHealthRangeMax + 1);
        public Brick                                BrickPrefab => brickPrefab;
        public int                                  Rows => rows;
        public int                                  Columns => columns;
        public float                                Spacing => spacing;
        public Sprite                               WinSprite => winSprite;
    }
}