using Unity.Services.Core;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID.Controllers
{
    public class UnityServicesManager : IInitializable
    {
#region IInitializable
        public async void Initialize()
        {
            await UnityServices.InitializeAsync();
            Debug.Log("<color=#2AFF00> UnityServicesManager initialized! </color>");
        }
#endregion
    }
}