using UnityEngine;

namespace MiniIT.ARKANOID.Settings
{
    [CreateAssetMenu(menuName = "Arkanoid/BallSettings", fileName = "BallSettings", order = 0)]
    public class BallSettings : ScriptableObject
    {
        [SerializeField] private float            ballRespawnTime;
        [SerializeField] private Vector2          startBallVector;
        public float                              BallRespawnTime => ballRespawnTime;
        public Vector2                            StartBallVector => startBallVector;
    }
}