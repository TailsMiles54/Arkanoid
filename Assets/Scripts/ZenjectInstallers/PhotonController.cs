using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Photon.Realtime;
using Fusion.Sockets;
using MiniIT.ARKANOID;
using MiniIT.ARKANOID.Settings;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PhotonController : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef               playerPrefab;
    
    private NetworkRunner                                   runner;
    private Dictionary<PlayerRef, NetworkObject>            spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    private GameController                                  gameController;

    public event Action                                     PlayerLeft;
    
    public void Setup(GameController gameController)
    {
        this.gameController = gameController;
    }
    
    public NetworkRunner                                    Runner => runner;
    private bool                                            clicked;
    
    public async void StartGame()
    {
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;
    
        var scene = SceneRef.FromIndex(3);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid) {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        if (!gameObject.TryGetComponent(out NetworkSceneManagerDefault networkSceneManagerDefault))
            networkSceneManagerDefault = gameObject.AddComponent<NetworkSceneManagerDefault>();
    
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionProperties = new Dictionary<string, SessionProperty>()
            {
                {"gameType", gameController.GameType.ToString()}
            },
            PlayerCount = 2,
            SceneManager = networkSceneManagerDefault,
            MatchmakingMode = MatchmakingMode.FillRoom,
        });
    }

    public async void SpawnPlayers(Vector3 position1, Vector3 position2)
    {
        foreach (var player in runner.ActivePlayers)
        {
            if(runner.IsServer)
            {
                Vector3 spawnPosition = new Vector3();
                Quaternion spawnQuaternion = new Quaternion();

                Debug.Log(player.AsIndex);
                if (player.AsIndex == 1)
                {
                    spawnPosition = position1;
                    spawnQuaternion = quaternion.identity;
                }
                else
                {
                    spawnPosition = position2;
                    spawnQuaternion = Quaternion.Euler(0, 0, 180);
                }
                
                NetworkObject networkPlayerObject = await runner.SpawnAsync(playerPrefab, spawnPosition, spawnQuaternion, player, async (runner, o) =>
                {
                    var platform = o.GetComponent<MultiplayerPlatformController>();
                    
                    await runner.SpawnAsync(SettingsProvider.Get<GameFieldSettings>().NetworkBallPrefab, platform.BallStartPosition, Quaternion.identity, player, async (runner, ball) =>
                    {
                        ball.GetComponent<NetworkedBall>().Setup(platform);
                    });
                });
                
                
                spawnedCharacters.Add(player, networkPlayerObject);
                runner.SetPlayerObject(player, networkPlayerObject);
            }
        }
    }

    public void Update()
    {
        clicked = Input.touches.Length > 0 || Input.GetMouseButton(0);
    }

    #region INetworkRunnerCallbacks
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            if(runner.ActivePlayers.Count() == 2)
                runner.LoadScene(SceneRef.FromIndex(3), LoadSceneMode.Single, setActiveOnLoad:true);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerLeft");
        PlayerLeft?.Invoke();
        
        if (spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            spawnedCharacters.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        if (Input.touches.Length > 0)
        {
            data.direction = Input.touches[0].position;
            input.Set(data);
        }

        if (Input.GetMouseButton(0))
        {
            data.direction = Input.mousePosition;
            input.Set(data);
        }

        data.buttons.Set(NetworkInputData.button, clicked);

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }
#endregion
}