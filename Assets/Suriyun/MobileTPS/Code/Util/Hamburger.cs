using System;
using System.Collections;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class Hamburger : MYBullet
    {
        [SerializeField] private int _hitCountAfterFall = 3;
        [SerializeField] private float _repeatRate = 1f;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private LayerMask _enemyLayer;

        private readonly Collider[] _results = new Collider[20];

        private bool _isGrounded;
        private Coroutine _explodeCoroutine;

        protected override void OnCollisionEnter(Collision collision)
        {
            if (_isGrounded == false)
            {
                _isGrounded = true;
                Rigidbody.isKinematic = true;
                Invoke(nameof(TurnOffKinematic), 0.12f);
                CancelInvoke(nameof(Die));


                _explodeCoroutine = StartCoroutine(Explode());
            }
        }

        private void TurnOffKinematic()
        {
            Rigidbody.isKinematic = false;
        }

        private IEnumerator Explode()
        {
            var repeatRate = new WaitForSeconds(_repeatRate);

            for (int i = 0; i < _hitCountAfterFall; i++)
            {
                var size = Physics.OverlapSphereNonAlloc(transform.position, _radius, _results, _enemyLayer);

                for (int j = 0; j < size; j++)
                {
                    if (_results[j] == null)
                        continue;

                    var enemy = _results[j].GetComponent<Enemy>();
                    enemy.ApplyDamage(Damage);
                }

                yield return repeatRate;
            }

            _explodeCoroutine = null;
            Die();
        }


        protected override IEnumerator Setup()
        {
            yield return null;
            yield return null;
            Rigidbody.useGravity = true;
            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(transform.forward * Speed, ForceMode.VelocityChange);
            Invoke(nameof(Die), LifeTime);
        }

        protected override void Die()
        {
            if (_explodeCoroutine != null)
            {
                StopCoroutine(_explodeCoroutine);
                _explodeCoroutine = null;
            }

            Rigidbody.isKinematic = false;
            _isGrounded = false;
            base.Die();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}