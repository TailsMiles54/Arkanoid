using System.Collections;
using UnityEngine;
using Zenject;

public class ProjectContext : MonoInstaller
{
    [SerializeField] private PhotonService _photonService;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameController>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<TestSystem>().AsCached().NonLazy();
        
        Container.BindInterfacesAndSelfTo<PhotonService>().FromComponentInNewPrefab(_photonService).AsSingle().NonLazy();
    }
}