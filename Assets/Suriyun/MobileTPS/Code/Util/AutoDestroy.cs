using UnityEngine;
using Suriyun.MobileTPS;

public class AutoDestroy : MonoBehaviour
{
    public float time = 3f;
    [SerializeField] private PoolObject _poolObject;

    private void Start()
    {
        if (_poolObject != null)
            _poolObject.ReturnToPool();
        else
            Destroy(this.gameObject, time);
    }
}