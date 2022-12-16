using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Suriyun.MobileTPS
{
    public class Enemy : MonoBehaviour
    {
        [HideInInspector] public Agent[] agents;
        [HideInInspector] public Animator animator;
        [HideInInspector] private string state;

        private NavMeshAgent agent;
        private Agent target;
        private Transform trans;

        public float target_switching_delay = 1.66f;

        public float atk_range = 0.66f;
        public float atk_delay = 0.66f;
        public float atk_dmg = 6;
        public float dmg_delay = 0.33f;

        public float hp = 100f;
        private bool is_dead;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            trans = transform;
            StartCoroutine(FindTarget());
            state = "Move";
            StartCoroutine(Move());
        }


        private void Update()
        {
            animator.SetFloat("hp", hp);
            animator.SetFloat("speed", agent.velocity.magnitude);
            if (hp <= 0f && !is_dead)
            {
                is_dead = true;
                StopAllCoroutines();
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            //Debug.Log ("Dead : " + gameObject.name);
            agent.Stop();
            Collider col = GetComponent<Collider>();
            col.enabled = false;
            yield return new WaitForSecondsRealtime(3f);
            Destroy(this.gameObject);
        }

        private IEnumerator FindTarget()
        {
            while (true)
            {
                agents = GameObject.FindObjectsOfType<Agent>();
                int nearest = 0;
                float min = 100;
                for (int i = 0; i < agents.Length; i++)
                {
                    float distance = Vector3.Distance(trans.position, agents[i].trans.position);
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
            while (state == "Move")
            {
                agent.destination = target.trans.position;
                float distance_to_target = Vector3.Distance(trans.position, target.trans.position);
                if (distance_to_target < atk_range)
                {
                    state = "Atk";
                }

                yield return 0;
            }

            StartCoroutine(state);
        }

        private IEnumerator Atk()
        {
            //Debug.Log ("Atk");
            animator.SetTrigger("atk");
            yield return new WaitForSeconds(dmg_delay);
            target.Hit(atk_dmg);
            yield return new WaitForSeconds(atk_delay);
            state = "Move";
            StartCoroutine(state);
        }
    }
}