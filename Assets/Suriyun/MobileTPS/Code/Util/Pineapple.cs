using System.Collections;
using Suriyun.MobileTPS.Code.Core;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class Pineapple : MYBullet
    {
        [Space(5)] [SerializeField] private float _explosionForce = 400f;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private PlaySoundsComponent _playSoundsComponent;

        private readonly Collider[] _results = new Collider[20];

        private bool _isGrounded;

        protected override void OnCollisionEnter(Collision collision)
        {
            if (_isGrounded == false)
            {
                _isGrounded = true;
                CancelInvoke(nameof(Die));
                Explode();
            }
        }

        private void Explode()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _results, _enemyLayer);

            for (var i = 0; i < count; i++)
            {
                if (_results[i] == null)
                    continue;

                _results[i].attachedRigidbody
                    .AddExplosionForce(_explosionForce, transform.position - Vector3.down * 0.5f, _radius);

                if (_results[i].TryGetComponent(out Enemy enemy))
                {
                    enemy.ApplyDamage(Damage);
                    enemy.StopVelocity();
                }
            }

            Die();
        }


        protected override IEnumerator Setup()
        {
            yield return null;
            yield return null;

            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(transform.forward * Speed, ForceMode.VelocityChange);
            _playSoundsComponent.Play("shoot");
            Invoke(nameof(Die), LifeTime);
        }

        protected override void Die()
        {
            Instantiate(HitEffect, transform.position, Quaternion.identity);

            _isGrounded = false;
            base.Die();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}