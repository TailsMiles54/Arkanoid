using System.Collections;
using UnityEngine;
using Zenject;

public class ProjectContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameController>().AsCached().NonLazy();
        
        Container.BindInterfacesAndSelfTo<TestSystem>().AsCached().NonLazy();
    }
}