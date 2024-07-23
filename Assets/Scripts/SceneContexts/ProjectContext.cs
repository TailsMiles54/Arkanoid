using System.Collections;
using UnityEngine;
using Zenject;

public class ProjectContext : MonoInstaller
{
    [SerializeField] private PhotonController            photonControllerPrefab;
    
    public override void InstallBindings()
    {
        var photonController = Container.InstantiatePrefabForComponent<PhotonController>(photonControllerPrefab);

        Container.BindInterfacesAndSelfTo<PhotonController>().FromInstance(photonController).AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<GameController>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<TestSystem>().AsCached().NonLazy();
    }
}