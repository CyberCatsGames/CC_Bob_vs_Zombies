using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _shootRotation;
        [SerializeField] private Pool _bulletPool;

        public Cooldown Cooldown => _cooldown;

        public void Shoot()
        {
            _bulletPool.GetFreeElement(_shootPoint);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _shootRotation.gameObject.SetActive(false);
            }
        }
    }
}