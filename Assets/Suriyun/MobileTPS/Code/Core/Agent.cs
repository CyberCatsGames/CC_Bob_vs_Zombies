using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
using Object = UnityEngine.Object;

namespace Suriyun.MobileTPS
{
    public class Agent : MonoBehaviour
    {
        public BehaviourControl behaviour;

        [HideInInspector] public Transform trans;

        public bool is_alive = true;

        public GameObject fx_on_hit;

        private HealthBarView _healthBar;

        public GameCamera GameCamera { get; private set; }

        private int _maxHealth;

        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            trans = transform;
            GameCamera = FindObjectOfType<GameCamera>();
            _healthBar = FindObjectOfType<HealthBarView>();

            _maxHealth = GameSession.Instance.PlayerInfo.Health;
            CurrentHealth = _maxHealth;
            behaviour.Init(this);
        }

        public void Hit(float damage)
        {
            CurrentHealth -= (int)damage;
            _healthBar.Change(damage);
            Instantiate(fx_on_hit, trans.position, fx_on_hit.transform.rotation);
        }

        public void GoToNextWave(Action callback = null)
        {
            behaviour.GoToNextWave(callback);
        }
    }

    [Serializable]
    public class BehaviourControl
    {
        [HideInInspector] public Button btn_fire;
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
        private bool _isNextWave;
        private Action _callback;

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
            if (btn_right == null)
                btn_right = GameObject.Find("+button.right").GetComponent<Button>();
            if (btn_left == null)
                btn_left = GameObject.Find("+button.left").GetComponent<Button>();

            parent.StartCoroutine(PseudoUpdate());
        }

        private IEnumerator PseudoUpdate()
        {
            while (true)
            {
                Update();
                if (parent.CurrentHealth <= 0)
                {
                    parent.is_alive = false;
                    Game.instance.EventGameOver.Invoke();
                }

                yield return 0;
            }
        }

        public void StartFiring()
        {
            parent.GameCamera.zoomed = true;

            if (parent.is_alive)
            {
                firing = true;
                _shooter.TryShoot();
            }
        }

        public void StopFiring()
        {
            firing = false;
            parent.GameCamera.zoomed = false;
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

            if (Game.instance.BlockInput == false)
            {
                #region :: Input Handler ::

                if (btn_left.pressed || Input.GetKeyDown(KeyCode.A))
                {
                    GoLeft();
                }

                if (btn_right.pressed || Input.GetKeyDown(KeyCode.D))
                {
                    GoRight();
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
            }
            else
            {
                _redPointsCount = Game.instance.ShootZone.MovePoints.Count;
                destination_index = Mathf.Clamp(destination_index, 0, _redPointsCount - 1);
                destination.position = Game.instance.ShootZone.MovePoints[destination_index].position;

                var position = agent.transform.position;

                Vector3 xzPosition = new Vector3(position.x, destination.position.y, position.z);
                if (destination.position == xzPosition)
                {
                    Game.instance.BlockInput = false;
                }
            }

            animator.SetBool("firing", firing);
            animator.SetFloat("speed", agent.velocity.magnitude);

            Move();

            animator.SetFloat("hp", parent.CurrentHealth);
            if (!parent.is_alive)
            {
                parent.StopAllCoroutines();
            }

            if (_shooter.Trajectory != null)
            {
                _shooter.Trajectory.gameObject.SetActive(firing);
            }

            _aim.gameObject.SetActive(_shooter.ShowAim);

            if (_isNextWave == true)
            {
                Vector3 xzPosition = new Vector3(agent.transform.position.x, 0f, agent.transform.position.z);
                Vector3 xzDestinationPosition = new Vector3(destination.position.x, 0f, destination.position.z);

                if (xzPosition == xzDestinationPosition)
                {
                    _callback?.Invoke();
                    _callback = null;
                    _isNextWave = false;
                }
            }
        }

        private void Move(Action callback = null)
        {
            _redPointsCount = Game.instance.ShootZone.MovePoints.Count;
            destination_index = Mathf.Clamp(destination_index, 0, _redPointsCount - 1);
            destination.position = Game.instance.ShootZone.MovePoints[destination_index].position;
            agent.destination = destination.position;
        }

        public void GoToNextWave(Action callback = null)
        {
            Game.instance.BlockInput = true;
            destination_index = 0;
            Move(callback);
            _isNextWave = true;
            _callback = callback;
        }

        private void UpdateCurrentPosition()
        {
            for (int i = 0; i < Game.instance.ShootZone.MovePoints.Count; i++)
            {
                if (Vector3.Distance(parent.trans.position, Game.instance.ShootZone.MovePoints[i].position) < 1f)
                {
                    current_index = i;
                }
            }
        }
    }
}