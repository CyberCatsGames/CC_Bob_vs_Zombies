using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class SauceThrower : Gun
    {
        [SerializeField] private float _maxDistance = 30f;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private ParticleSystem _shootEffect;

        public override void Shoot()
        {
            bool result = Physics.Raycast(ShootPoint.position, ShootPoint.forward, out RaycastHit hit, _maxDistance,
                _targetLayer);

            if (_shootEffect.isPlaying == false)
                _shootEffect.Play();

            if (result == true)
            {
                var enemy = hit.collider.GetComponent<Enemy>();
                enemy.ApplyDamage(Damage);
            }
        }

        protected override void UpdateTrajectory()
        {
            Trajectory.ShowTrajectory(ShootPoint, _maxDistance);
        }
    }
}