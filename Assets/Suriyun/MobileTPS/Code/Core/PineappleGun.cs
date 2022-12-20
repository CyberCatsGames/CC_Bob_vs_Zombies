using System.Collections;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class PineappleGun : Gun
    {
        [SerializeField] private GameObject _notRealBullet;

        private Coroutine _coroutine;

        public override void Shoot()
        {
            base.Shoot();
            StartCoroutine(TurnOnBullet());
        }

        private IEnumerator TurnOnBullet()
        {
            _notRealBullet.SetActive(false);
            yield return new WaitForSeconds(Cooldown.Value - 0.04f);
            _notRealBullet.SetActive(true);
        }
    }
}