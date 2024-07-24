using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PhotonService : MonoBehaviour, IInitializable
{
    [SerializeField] private PhotonController       photonManagerPrefab;

    private PhotonController                        photonController;
    private GameController                          gameController;
    public PhotonController                         PhotonController => photonController;

    [Inject]
    public void Construct(GameController gameController)
    {
        this.gameController = gameController;
    }
    
    public void Initialize()
    {
        NewPhotonManager();
        Debug.Log("<color=#2AFF00> PhotonService initialized! </color>");
        SceneManager.LoadScene("MenuScene");
    }

    public void NewPhotonManager()
    {
        photonController = Instantiate(photonManagerPrefab, transform);
        photonController.Setup(gameController);
    }
}