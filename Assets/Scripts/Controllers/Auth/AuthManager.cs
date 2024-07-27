using MiniIT.ARKANOID.Save;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MiniIT.ARKANOID.Controllers.Auth
{
    public class AuthManager : IInitializable
    {
        private ISaveManager            saveManager;

        [Inject]
        public void Construct(ISaveManager saveManager)
        {
            this.saveManager = saveManager;
        }
        
#region IInitializable
        public void Initialize()
        {
            Debug.Log("<color=#2AFF00> AuthManager initialized! </color>");
            SceneManager.LoadScene(1);
            PlayerAccountService.Instance.SignedIn += OnSignedIn;
        }
#endregion
 
        public async void StartSignIn()
        {
            try
            {
                await PlayerAccountService.Instance.StartSignInAsync();
            }
            catch (AuthenticationException ex)
            {
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                Debug.LogException(ex);
            }
        }
 
        private async void OnSignedIn()
        {
            Debug.Log("Player Account Access token " + PlayerAccountService.Instance.AccessToken);
            await AuthenticationService.Instance.SignInWithUnityAsync(PlayerAccountService.Instance.AccessToken);
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
}