using UnityEngine;

namespace MiniIT.ARKANOID.UIElements
{
    public class BasePopup : MonoBehaviour
    {
        public virtual void Hide()
        {
            Destroy(gameObject);
        }
    }
}