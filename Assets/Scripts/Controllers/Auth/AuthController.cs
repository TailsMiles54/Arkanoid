using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID.Controllers.Auth
{
    public class AuthController : MonoBehaviour
    {
        private AuthManager         authManager;

        [Inject]
        public void Construct(AuthManager authManager)
        {
            this.authManager = authManager;
        }
    
        public void TryAuth()
        {
            authManager.StartSignIn();
        }
    }
}
