using System;
using System.Collections;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class Hamburger : MYBullet
    {
        [SerializeField] private float _radius = 2f;
        [SerializeField] private LayerMask _enemyLayer;

        private readonly Collider[] _results = new Collider[20];

        private bool _exploded;
        private bool _isGrounded;

        protected override void OnCollisionEnter(Collision collision)
        {
            if (_isGrounded == false)
            {
                _isGrounded = true;
                Rigidbody.isKinematic = true;
                Invoke(nameof(TurnOffKinematic), 0.12f);
                CancelInvoke(nameof(Die));
            }
        }

        private void TurnOffKinematic()
        {
            Rigidbody.isKinematic = false;
        }

        public void TryExplode()
        {
            if (_exploded == true)
                return;

            _exploded = true;

            var size = Physics.OverlapSphereNonAlloc(transform.position, _radius, _results, _enemyLayer);

            for (int j = 0; j < size; j++)
            {
                if (_results[j] == null)
                    continue;

                var enemy = _results[j].GetComponent<Enemy>();
                enemy.ApplyDamage(Damage);
            }

            Instantiate(HitEffect, transform.position, Quaternion.identity);
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
            TryExplode();
            Rigidbody.isKinematic = false;
            _isGrounded = false;
            _exploded = false;
            base.Die();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}