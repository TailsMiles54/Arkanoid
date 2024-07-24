using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class MenuSceneContext : MonoInstaller
    {
        [SerializeField] private PhotonController            photonControllerPrefab;
        
        public override void InstallBindings()
        {
            BindPhotonController();
        }

        public void BindPhotonController()
        {
            var photonController = Container.InstantiatePrefabForComponent<PhotonController>(photonControllerPrefab);
            Container.BindInterfacesAndSelfTo<PhotonController>().FromInstance(photonController).AsCached().NonLazy();
        }
    }
}
