using System;

namespace Suriyun.MobileTPS
{
    public class BulletPool : Pool<MYBullet>
    {
        public void Init(float speed, float damage)
        {
            if (speed < 0f)
                throw new ArgumentException("Speed less then 0!");

            foreach (var bullet in PoolContainer)
            {
                bullet.SetSpeed(speed);
                bullet.SetDamage(damage);
            }
        }
    }
}