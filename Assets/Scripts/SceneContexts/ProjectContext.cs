using System.Collections;
using UnityEngine;
using Zenject;

public class ProjectContext : MonoInstaller
{
    [SerializeField] private SoundController            soundControllerPrefab; 
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameController>().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<SoundController>().FromComponentInNewPrefab(soundControllerPrefab).AsSingle().NonLazy();
    }
}