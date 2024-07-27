using MiniIT.ARKANOID.Controllers;
using MiniIT.ARKANOID.Controllers.Auth;
using MiniIT.ARKANOID.Save;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID.ZenjectContexts
{
    public class ProjectContext : MonoInstaller
    {
        [SerializeField] private SoundController                soundControllerPrefab; 
        public override void InstallBindings()
        {
#if !UNITY_WEBGL
            Container.BindInterfacesTo<UnityServicesManager>().AsCached();
            Container.BindInterfacesAndSelfTo<AuthManager>().AsCached();
            Container.Bind<ISaveManager>().To<SaveManager>().AsCached();
#else
            Container.Bind<ISaveManager>().To<WebSaveManager>().AsCached();
            SceneManager.LoadScene(2);
#endif
            Container.BindInterfacesAndSelfTo<GameController>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<SoundController>().FromComponentInNewPrefab(soundControllerPrefab).AsSingle().NonLazy();
        }
    }
}