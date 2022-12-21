using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private GunSwitcher _switcher;

        private Gun Gun => _switcher.CurrentGun;

        private float _timer;

        public bool ShowAim => Gun.ShowAim;

        public TrajectoryRenderer Trajectory => Gun.Trajectory != null ? Gun.Trajectory : null;

        public void TryShoot()
        {
            if (Gun.Cooldown.IsReady == true)
            {
                Gun.Shoot();
                
                if (Gun.ShootEffect != null && Gun.ShootEffect.isPlaying == false)
                    Gun.ShootEffect.Play();
                
                Gun.Cooldown.Reset();
            }
        }
    }
}