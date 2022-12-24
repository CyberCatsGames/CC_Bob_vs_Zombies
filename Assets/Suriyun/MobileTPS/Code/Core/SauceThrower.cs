using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class SauceThrower : Gun {
        public AudioSource SouceSound;
        public override void Shoot() {
            base.Shoot();
            SouceSound.Play();
        }
    }
}