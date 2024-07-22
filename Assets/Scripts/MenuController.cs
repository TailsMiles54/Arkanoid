using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniIT.ARKANOID
{
    public class MenuController : MonoBehaviour
    {
        public void SoloGameStart()
        {
            SceneManager.LoadScene("SoloGameScene");
        }

        public void MultiplayerGameStart()
        {
            SceneManager.LoadScene("MultiplayerGameScene");
        }

        public void ShowSettings()
        {
            
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
