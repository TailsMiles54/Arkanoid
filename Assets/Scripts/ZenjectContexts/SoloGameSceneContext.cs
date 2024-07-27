using System.Collections;
using System.Collections.Generic;
using MiniIT.ARKANOID.Controllers;
using MiniIT.ARKANOID.Gameplay;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class SoloGameSceneContext : MonoInstaller
    {
        [SerializeField] private GameUIController            scoreController;
        [SerializeField] private GameField                  gameField;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameUIController>().FromInstance(scoreController).AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<GameField>().FromInstance(gameField).AsCached().NonLazy();
        }
    }
}
