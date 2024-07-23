using Fusion;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public struct NetworkInputData : INetworkInput
    {
        public const byte           button = 1;

        public NetworkButtons       buttons;
        public Vector3              direction;
    }
}