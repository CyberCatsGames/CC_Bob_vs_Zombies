using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

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
        private Agent _agent;

        public static Game instance;
        public bool BlockInput;

        private Spawner Spawner => _spawner;

        public AttackPoints ShootZone => _mapData.ShootZones[_currentShootZoneIndex];

        private const int TARGET_FRAME_RATE = 60;

        public UnityEvent EventGameStart;
        public UnityEvent EventGameRestart;
        public UnityEvent EventGameOver;

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
            GameSession.Instance.Save(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ShowGameOverMenu()
        {
            EventGameOver.Invoke();
            Spawner.StopSpawning();
        }

        private void OnWaveFinished()
        {
            _nextWavePanel.gameObject.SetActive(true);
        }

        private void OnNextWave()
        {
            _currentShootZoneIndex++;

            _currentShootZoneIndex = Mathf.Clamp(
                _currentShootZoneIndex,
                0,
                _mapData.ShootZones.Count - 1);

            GameSession.Instance.Save(_currentShootZoneIndex);
            _nextWavePanel.gameObject.SetActive(false);
            _agent.GoToNextWave(() => _spawner.StartSpawning());
        }
    }

    [Serializable]
    public class AttackPoints
    {
        public List<Transform> MovePoints;
    }
}