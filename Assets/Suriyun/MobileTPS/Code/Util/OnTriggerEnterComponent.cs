using UnityEngine;
using UnityEngine.Events;

namespace Suriyun.MobileTPS
{
    public class OnTriggerEnterComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onTrigger;
        [SerializeField] private LayerMask _targetLayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.IsInLayer(_targetLayer))
            {
                _onTrigger?.Invoke();
            }
        }
    }
}