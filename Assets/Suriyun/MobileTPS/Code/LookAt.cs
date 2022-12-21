using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class LookAt : MonoBehaviour
    {
        public Vector3 offset;
        private GameObject target;

        private void Start()
        {
            target = GameObject.Find("+target");
        }

        private void Update()
        {
            transform.LookAt(target.transform);
            transform.Rotate(offset);
        }
    }
}