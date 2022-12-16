using UnityEngine;

namespace Suriyun.MobileTPS
{
    [RequireComponent(typeof(PoolObject))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitEffect;
        public float damage = 6.66f;
        [Range(0.0f, 100.0f)] public float accuraycy = 99f;

        public float speed = 128;
        public float lifetime = 3f;

        private Rigidbody rig;
        private PoolObject _poolObject;
        private float dev = 0.016f;

        // private void Awake()
        // {
        //     _poolObject = GetComponent<PoolObject>();
        // }
        //
        // private void Start()
        // {
        //     // Calculate accuracy //
        //     Vector3 rand = Vector3.zero;
        //     float offset = 100f - accuraycy;
        //     rand += transform.right * Random.Range(-offset, offset);
        //     rand += transform.up * Random.Range(-offset, offset);
        //     rand = rand.normalized * dev;
        //
        //     // Fire bullet //
        //     rig = GetComponent<Rigidbody>();
        //     rig.AddForce((transform.forward + rand) * speed, ForceMode.VelocityChange);
        //
        //     // Set Bullet lifetime //
        //     // StartCoroutine(Expire(lifetime));
        //     Invoke(nameof(Expire), lifetime);
        //
        //     // Set pointlight duration a bullet is fired //
        //     StartCoroutine(ExpireLight());
        //
        //     print("Create");
        // }
        //
        // private void Expire()
        // {
        //     _poolObject.ReturnToPool();
        // }
        //
        // private IEnumerator ExpireLight()
        // {
        //     yield return 0; //new WaitForSeconds (0.08f);
        //     // Destroy(GetComponent<Light>());
        //     _poolObject.ReturnToPool();
        // }
        //
        // private void OnCollisionEnter(Collision col)
        // {
        //     _poolObject.ReturnToPool();
        //     print(col.gameObject.name);
        //     rig.useGravity = true;
        //     if (col.gameObject.tag == "Enemy")
        //     {
        //         _hitEffect.transform.position = col.contacts[0].point;
        //         _hitEffect.Play();
        //         // Instantiate(fx_on_hit, col.contacts[0].point, fx_on_hit.transform.rotation);
        //         var enemy = col.gameObject.GetComponent<Enemy>();
        //         enemy.hp -= damage;
        //         _poolObject.ReturnToPool();
        //     }
        // }
        //
        // private void Die()
        // {
        //     
        // }
        // private void OnDestroy()
        // {
        //     print("I destroyed");
        // }
    }
}