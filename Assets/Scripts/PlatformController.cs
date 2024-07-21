using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.ARCANOID
{
    public class PlatformController : MonoBehaviour
    {
        public float            playerVelocity;
        private Vector3         playerPosition;
        private int             boundary = 2;
     
        // используйте этот метод для инициализации
        void Start () {
            // получим начальную позицию платформы
            playerPosition = gameObject.transform.position;
        }

        // Update вызывается при отрисовке каждого кадра игры
        void Update () {
            // горизонтальное движение
            playerPosition.x = Mathf.Clamp(playerPosition.x + Input.GetAxis ("Horizontal") * playerVelocity, -boundary, boundary);
 
            // выход из игры
            if (Input.GetKeyDown(KeyCode.Escape)){
                Application.Quit();
            }
 
            // обновим позицию платформы
            transform.position = playerPosition;
        }
    }
}
