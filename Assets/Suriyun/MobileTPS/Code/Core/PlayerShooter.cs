using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Gun _gun;

        private float _timer;

        public bool ShowAim => _gun.ShowAim;

        public TrajectoryRenderer Trajectory => _gun.Trajectory != null ? _gun.Trajectory : null;

        public void TryShoot()
        {
            if (_gun.Cooldown.IsReady == true)
            {
                _gun.Shoot();
                _gun.Cooldown.Reset();
            }
        }
    }
}