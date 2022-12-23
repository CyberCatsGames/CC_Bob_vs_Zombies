using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using TMPro;

namespace Suriyun.MobileTPS
{
    [Serializable]
    public class MapData
    {
        public List<AttackPoints> ShootZones;
    }

    public class Game : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private MapData _mapData;
        [SerializeField] private RectTransform _nextWavePanel;
        [SerializeField] private UnityEngine.UI.Button _nextWaveButton;
        [SerializeField] private Agent _patricTemplate;

        private int _currentShootZoneIndex;
        private int _coinsCount;
        private GameCamera _gameCamera;

        private Agent _agent;

        public static Game instance;
        public bool BlockInput;

        private Spawner Spawner => _spawner;

        public AttackPoints ShootZone => _mapData.ShootZones[_currentShootZoneIndex];

        public GunSwitcher GunSwitcher { get; private set; }

        private const int TARGET_FRAME_RATE = 60;

        public UnityEvent EventGameStart;
        public UnityEvent EventGameRestart;
        public UnityEvent EventGameOver;

        [Space(10)] [SerializeField] private TMP_Text _currentKillsCountTextView;
        [SerializeField] private TMP_Text _totalKillsCountView;
        [SerializeField] private TMP_Text _coinsCountTextView;

        [Space(10)] [SerializeField] private TMP_Text _currentKillsCountTextView1;
        [SerializeField] private TMP_Text _totalKillsCountView1;
        [SerializeField] private TMP_Text _coinsCountTextView1;

        private void Awake()
        {
            instance = this;
            Application.targetFrameRate = TARGET_FRAME_RATE;
        }

        private void OnEnable()
        {
            _spawner.WaveFinished += OnWaveFinished;
            _nextWaveButton.onClick.AddListener(OnNextWave);
        }

        private void Start()
        {
            _gameCamera = FindObjectOfType<GameCamera>();
            var playerInfo = GameSession.Instance.PlayerInfo;

            int index = playerInfo.CurrentWaveIndex;
            _currentShootZoneIndex = GameSession.Instance.PlayerInfo.CurrentWaveIndex;

            var targetPosition = _mapData
                .ShootZones[index]
                .MovePoints[0].position;

            _agent = Instantiate(
                _patricTemplate,
                targetPosition,
                Quaternion.identity
            );

            GunSwitcher = _agent.GetComponentInChildren<GunSwitcher>();
        }

        private void OnDisable()
        {
            _spawner.WaveFinished -= OnWaveFinished;
            _nextWaveButton.onClick.RemoveListener(OnNextWave);
        }

        public void GameStart()
        {
            EventGameStart.Invoke();
            Spawner.StartSpawning();
        }

        public void GameRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void RestartAllGame()
        {
            GameSession.Instance.PlayerInfo.CurrentWaveIndex = 0;
            GameSession.Instance.SaveZonePosition(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ShowGameOverMenu()
        {
            EventGameOver.Invoke();
            Spawner.StopSpawning();
            _gameCamera.BlockRotate();
        }

        private void OnWaveFinished()
        {
            SetTextsValues();
            _nextWavePanel.gameObject.SetActive(true);
            _gameCamera.BlockRotate();
        }

        private void OnNextWave()
        {
            _gameCamera.UnblockRotate();
            _currentShootZoneIndex++;
            GameSession.Instance.SaveZonePosition(_currentShootZoneIndex);

            _currentShootZoneIndex = Mathf.Clamp(
                _currentShootZoneIndex,
                0,
                _mapData.ShootZones.Count - 1);

            _nextWavePanel.gameObject.SetActive(false);
            _agent.GoToNextWave(() => _spawner.StartSpawning());
        }

        public void SetTextsValues()
        {
            _currentKillsCountTextView.text = _spawner.KillsCountSession.ToString();
            _totalKillsCountView.text = GameSession.Instance.PlayerInfo.KillsCount.ToString();
            _coinsCountTextView.text = _spawner.CoinsCountSession.ToString();

            _currentKillsCountTextView1.text = _spawner.KillsCountSession.ToString();
            _totalKillsCountView1.text = GameSession.Instance.PlayerInfo.KillsCount.ToString();
            _coinsCountTextView1.text = _spawner.CoinsCountSession.ToString();
        }
    }

    [Serializable]
    public class AttackPoints
    {
        public List<Transform> MovePoints;
    }
}