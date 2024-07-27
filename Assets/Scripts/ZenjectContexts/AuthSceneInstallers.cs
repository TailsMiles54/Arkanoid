using MiniIT.ARKANOID.Controllers.Auth;
using UnityEngine;
using Zenject;

public class AuthSceneInstallers : MonoInstaller
{
    [SerializeField] private AuthController             authManager;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AuthController>().FromComponentInNewPrefab(authManager).AsSingle();
    }
}
