using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerInfo _playerInfo;

        public static GameSession Instance { get; private set; }

        public PlayerInfo PlayerInfo => _playerInfo;

        private const string WaveIndex = "WaveIndex";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadLastSave();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Save(int currentShootZoneIndex)
        {
            _playerInfo.CurrentWaveIndex = currentShootZoneIndex;
            PlayerPrefs.SetInt(WaveIndex, currentShootZoneIndex);
        }

        public void LoadLastSave()
        {
            _playerInfo = new PlayerInfo
            {
                CurrentWaveIndex = PlayerPrefs.GetInt(WaveIndex, _playerInfo.CurrentWaveIndex)
            };
        }
    }
}