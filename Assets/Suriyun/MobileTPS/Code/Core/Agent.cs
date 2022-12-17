using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Collections;
using System;
using Object = UnityEngine.Object;

namespace Suriyun.MobileTPS
{
    public class Agent : MonoBehaviour
    {
        public GameCamera game_camera;

        public BehaviourControl behaviour;
        // [SerializeField] private Pool _effectsPool;

        [HideInInspector] public Transform trans;

        public float hp = 15;
        public bool is_alive = true;

        public GameObject fx_on_hit;

        public UnityEvent ActorDiedEvent;

        private void Awake()
        {
            trans = transform;
            behaviour.Init(this);
        }

        public void Hit(float damage)
        {
            hp -= damage;
            // _effectsPool.GetFreeElement(trans.position, fx_on_hit.transform.rotation);
            Instantiate(fx_on_hit, trans.position, fx_on_hit.transform.rotation);
        }
    }

    [Serializable]
    public class BehaviourControl
    {
        [HideInInspector] public Button btn_fire;
        [HideInInspector] public Button btn_hide;
        [HideInInspector] public Button btn_right;
        [HideInInspector] public Button btn_left;

        private Agent parent;

        [HideInInspector] public Transform destination;
        private int destination_index;
        private int current_index;
        private AimTag _aim;
        private int _redPointsCount;

        [HideInInspector] public bool firing;
        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public Animator animator;
        [SerializeField] private PlayerShooter _shooter;

        public void Init(Agent parent)
        {
            this.parent = parent;
            destination = GameObject.Find("+destination").transform;
            animator = parent.GetComponent<Animator>();
            agent = parent.GetComponent<NavMeshAgent>();
            _aim = Object.FindObjectOfType<AimTag>();
            _aim.gameObject.SetActive(false);

            if (btn_fire == null)
                btn_fire = GameObject.Find("+button.fire").GetComponent<Button>();
            if (btn_hide == null)
                btn_hide = GameObject.Find("+button.hide").GetComponent<Button>();
            if (btn_right == null)
                btn_right = GameObject.Find("+button.right").GetComponent<Button>();
            if (btn_left == null)
                btn_left = GameObject.Find("+button.left").GetComponent<Button>();

            parent.StartCoroutine(PseudoUpdate());
        }

        private IEnumerator Die()
        {
            yield return new WaitForSecondsRealtime(2f);
        }

        private IEnumerator PseudoUpdate()
        {
            while (true)
            {
                Update();
                if (parent.hp <= 0)
                {
                    parent.is_alive = false;
                    Game.instance.EventGameOver.Invoke();
                }

                yield return 0;
            }
        }

        public void StartFiring()
        {
            parent.game_camera.zoomed = true;

            if (parent.is_alive)
            {
                firing = true;
                _shooter.TryShoot();
            }
        }

        public void StopFiring()
        {
            firing = false;
            parent.game_camera.zoomed = false;
        }

        public void Hide()
        {
            if (parent.is_alive)
            {
                firing = false;
                agent.Stop();
            }
        }

        public void GoLeft()
        {
            if (parent.is_alive)
            {
                agent.Resume();
                if (current_index < destination_index)
                {
                    current_index = destination_index;
                }

                destination_index = current_index - 1;
            }
        }

        public void GoRight()
        {
            if (parent.is_alive)
            {
                agent.Resume();
                if (current_index > destination_index)
                {
                    current_index = destination_index;
                }

                destination_index = current_index + 1;
            }
        }

        public void Update()
        {
            UpdateCurrentPosition();

            #region :: Input Handler ::

            if (btn_left.pressed || Input.GetKeyDown(KeyCode.A))
            {
                GoLeft();
            }

            if (btn_right.pressed || Input.GetKeyDown(KeyCode.D))
            {
                GoRight();
            }

            if (btn_hide.pressed || Input.GetKeyDown(KeyCode.S))
            {
                Hide();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                StartFiring();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopFiring();
            }

            #endregion

            animator.SetBool("firing", firing);
            animator.SetFloat("speed", agent.velocity.magnitude);

            _redPointsCount = Game.instance.map_data.red_move_pos.Count;

            if (destination_index < 0)
            {
                destination_index = _redPointsCount - 1;
            }

            destination_index %= _redPointsCount;
            destination.position = Game.instance.map_data.red_move_pos[destination_index].position;
            agent.destination = destination.position;

            animator.SetFloat("hp", parent.hp);
            if (!parent.is_alive)
            {
                parent.StopAllCoroutines();
            }

            _aim.gameObject.SetActive(_shooter.ShowAim);
        }

        private void UpdateCurrentPosition()
        {
            for (int i = 0; i < Game.instance.map_data.red_move_pos.Count; i++)
            {
                if (Vector3.Distance(parent.trans.position, Game.instance.map_data.red_move_pos[i].position) < 1f)
                {
                    current_index = i;
                }
            }
        }
    }
}