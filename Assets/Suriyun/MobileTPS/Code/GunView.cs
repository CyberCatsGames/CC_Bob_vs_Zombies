using TMPro;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class GunView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bulletCount;

        public TMP_Text BulletCount => _bulletCount;
    }
}