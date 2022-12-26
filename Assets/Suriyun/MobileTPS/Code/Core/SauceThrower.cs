namespace Suriyun.MobileTPS
{
    public class SauceThrower : Gun
    {
        public override void Shoot()
        {
            base.Shoot();
            SouceSound.Play("shoot");
        }
    }
}