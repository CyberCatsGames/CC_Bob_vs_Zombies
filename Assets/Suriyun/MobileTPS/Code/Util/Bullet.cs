using System.Collections;
using UnityEngine;

namespace Suriyun.MobileTPS {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PoolObject))]
    public class Bullet : MonoBehaviour {
        [SerializeField] protected ParticleSystem HitEffect;
        [SerializeField] private float _lifeTime = 1.5f;

        private PoolObject _poolObject;
        private Coroutine _dieCoroutine;

        protected Rigidbody Rigidbody;

        protected float LifeTime => _lifeTime;

        protected float Speed { get; private set; } = 120f;

        protected float Damage { get; private set; } = 6f;

        private void Awake() {
            Rigidbody = GetComponent<Rigidbody>();
            _poolObject = GetComponent<PoolObject>();
        }

        private void OnEnable() {
            StartCoroutine(Setup());
        }

        protected virtual void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.TryGetComponent(out Enemy enemy)) {
                enemy.ApplyDamage(Damage);
                var newEffect = Instantiate(HitEffect, collision.contacts[0].point, Quaternion.identity);
                CancelInvoke(nameof(Die));
            }

            Die();
        }

        protected virtual void Die() {
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            _poolObject.ReturnToPool();
        }

        protected virtual IEnumerator Setup() {
            yield return null;
            yield return null;
            Rigidbody.useGravity = true;
            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(transform.forward * Speed, ForceMode.VelocityChange);
            Invoke(nameof(Die), _lifeTime);
        }

        public void SetSpeed(float value) {
            Speed = value;
        }

        public void SetDamage(float damage) {
            Damage = damage;
        }
    }
}