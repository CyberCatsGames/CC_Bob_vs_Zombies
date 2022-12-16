using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Gun _gun;

        private Animator _animator;
        private bool _isShooting;
        private float _timer;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void TryShoot()
        {
            _isShooting = true;
            
            if (_gun.Cooldown.IsReady == true)
            {
                _gun.Shoot();
                _gun.Cooldown.Reset();
            }
        }
    }
}