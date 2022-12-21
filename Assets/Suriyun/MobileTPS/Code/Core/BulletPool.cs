using System;

namespace Suriyun.MobileTPS
{
    public class BulletPool : Pool<MYBullet>
    {
        private float _speed;
        private float _damage;

        public void Init(float speed, float damage)
        {
            if (speed < 0f)
                throw new ArgumentException("Speed less then 0!");

            foreach (var bullet in PoolContainer)
            {
                bullet.SetSpeed(speed);
                bullet.SetDamage(damage);
            }

            _speed = speed;
            _damage = damage;
        }

        protected override MYBullet CreateElement(bool isActiveByDefault = false)
        {
            var result = base.CreateElement(isActiveByDefault);
            result.SetSpeed(_speed);
            result.SetDamage(_damage);
            return result;
        }
    }
}