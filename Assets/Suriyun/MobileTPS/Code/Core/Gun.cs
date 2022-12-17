using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private float _speed = 50f;
        [SerializeField] private float _damage = 50f;
        [Space(10)] [SerializeField] private Transform _shootPoint;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private TrajectoryRenderer _trajectory;
        [SerializeField] private bool _showAim;

        public bool ShowAim => _showAim;
        
        public Cooldown Cooldown => _cooldown;

        private void Start()
        {
            _bulletPool.Init(_speed, _damage);
        }

        public void Shoot()
        {
            _bulletPool.GetFreeElement(_shootPoint);
        }

        private void Update()
        {
            _trajectory.ShowTrajectory(_shootPoint.position, _speed * _shootPoint.forward);
        }
    }
}