using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Suriyun.MobileTPS
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _health = 100f;
        [HideInInspector] public Agent[] agents;
        [HideInInspector] public Animator _animator;
        private string _state;

        private NavMeshAgent _agent;
        private Agent target;

        public float target_switching_delay = 1.66f;

        public float atk_range = 0.66f;
        public float atk_delay = 0.66f;
        public float atk_dmg = 6;
        public float dmg_delay = 0.33f;

        private bool _isDead;
        private Coroutine _stopVelocityCoroutine;

        public event Action<Enemy> Died;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            StartCoroutine(FindTarget());
            _state = "Move";
            StartCoroutine(Move());
            _animator.SetFloat("hp", _health);
        }

        private void Update()
        {
            _animator.SetFloat("speed", _agent.velocity.magnitude);
        }

        public void ApplyDamage(float value)
        {
            if (_isDead == true)
                return;

            _health -= value;
            _animator.SetFloat("hp", _health);

            if (_health <= 0f)
            {
                _isDead = true;
                StopAllCoroutines();
                StartCoroutine(Die());
                Died?.Invoke(this);
            }
        }

        private IEnumerator Die()
        {
            _agent.isStopped = true;
            GetComponent<Collider>().enabled = false;
            _animator.SetBool("IsDied", true);
            yield return new WaitForSecondsRealtime(3f);
            Destroy(gameObject);
        }

        private IEnumerator StopVelocityInTime()
        {
            yield return new WaitForSeconds(1f);
            _rigidbody.velocity = Vector3.zero;
        }

        public void StopVelocity()
        {
            if (_stopVelocityCoroutine != null)
                return;

            _stopVelocityCoroutine = StartCoroutine(StopVelocityInTime());
        }

        private IEnumerator FindTarget()
        {
            while (true)
            {
                agents = FindObjectsOfType<Agent>();
                int nearest = 0;
                float min = 100;
                for (int i = 0; i < agents.Length; i++)
                {
                    float distance = Vector3.Distance(transform.position, agents[i].trans.position);
                    if (distance < min)
                    {
                        nearest = i;
                        min = distance;
                    }
                }

                target = agents[nearest];
                //agent.destination = target.trans.position;
                yield return new WaitForSeconds(target_switching_delay);
            }
        }

        private IEnumerator Move()
        {
            while (_state == "Move")
            {
                _agent.destination = target.trans.position;
                float distance_to_target = Vector3.Distance(transform.position, target.trans.position);
                if (distance_to_target < atk_range)
                {
                    _state = "Atk";
                }

                yield return 0;
            }

            StartCoroutine(_state);
        }

        private IEnumerator Atk()
        {
            //Debug.Log ("Atk");
            _animator.SetTrigger("atk");
            yield return new WaitForSeconds(dmg_delay);
            target.Hit(atk_dmg);
            yield return new WaitForSeconds(atk_delay);
            _state = "Move";
            StartCoroutine(_state);
        }
    }
}