using System.Collections;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PoolObject))]
    public class MYBullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private float _damage = 1f;

        [SerializeField] private float _speed = 120f;
        [SerializeField] private float _lifeTime = 1.5f;

        private Rigidbody _rigidbody;
        private PoolObject _poolObject;
        private Coroutine _dieCoroutine;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _poolObject = GetComponent<PoolObject>();
        }

        private void OnEnable()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(transform.forward * _speed, ForceMode.VelocityChange);
            _dieCoroutine = StartCoroutine(nameof(DieInTime));
        }

        private void OnDisable()
        {
            if (_dieCoroutine != null)
                StopCoroutine(_dieCoroutine);

            _dieCoroutine = null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            _rigidbody.useGravity = true;

            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                print("Enemy");
                enemy.hp -= _damage;
                _hitEffect.transform.position = collision.contacts[0].point;
                _hitEffect.Play();
                Die();
            }
        }

        private IEnumerator DieInTime()
        {
            yield return new WaitForSeconds(_lifeTime);
            Die();
            _dieCoroutine = null;
        }

        private void Die()
        {
            _rigidbody.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            _poolObject.ReturnToPool();
        }
    }
}