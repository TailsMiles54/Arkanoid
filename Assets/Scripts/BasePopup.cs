using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BasePopup : MonoBehaviour
    {
        public virtual void Hide()
        {
            Destroy(gameObject);
        }
    }
}