using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class MultiplayerGameSceneContext : MonoInstaller
    {
        [SerializeField] private BaseGameUIController         scoreController;
        [SerializeField] private BaseGameField                gameField;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BaseGameUIController>().FromInstance(scoreController).AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<BaseGameField>().FromInstance(gameField).AsCached().NonLazy();
        }
    }
}
