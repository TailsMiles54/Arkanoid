using UnityEngine;

namespace MiniIT.ARKANOID.Settings
{
    [CreateAssetMenu(menuName = "Arkanoid/PlayerSettings", fileName = "PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private float              playerSpeed;
        public float                                PlayerSpeed => playerSpeed;
    }
}