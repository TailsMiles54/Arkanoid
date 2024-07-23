using UniRx;
using UnityEngine;
using Zenject;

public class TestSystem : IInitializable
{
    public void Initialize()
    {
        
#if UNITY_EDITOR
        Observable.EveryUpdate().Where(x => Input.GetKey(KeyCode.T) && Input.GetKeyDown(KeyCode.P)).Subscribe(_ =>
            {
                
            }
        );
#endif
        
    }
}