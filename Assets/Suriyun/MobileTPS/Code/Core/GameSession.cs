using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerInfo _playerInfo;

        public static GameSession Instance { get; private set; }

        public PlayerInfo PlayerInfo => _playerInfo;

        private const string WaveIndex = "WaveIndex";
        private const string CoinsTag = "Coins";
        private const string KillsTag = "KillsCount";

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

        public void SaveZonePosition(int currentShootZoneIndex)
        {
            _playerInfo.CurrentWaveIndex = currentShootZoneIndex;
            PlayerPrefs.SetInt(WaveIndex, currentShootZoneIndex);
        }

        public void SaveCoins()
        {
            PlayerPrefs.SetInt(CoinsTag, PlayerInfo.Coins);
        }

        public void SaveKills()
        {
            PlayerPrefs.SetInt(KillsTag, PlayerInfo.KillsCount);
        }

        public void LoadLastSave()
        {
            _playerInfo = new PlayerInfo
            {
                CurrentWaveIndex = PlayerPrefs.GetInt(WaveIndex, _playerInfo.CurrentWaveIndex),
                Coins = PlayerPrefs.GetInt(WaveIndex, _playerInfo.Coins),
                KillsCount = PlayerPrefs.GetInt(KillsTag, _playerInfo.KillsCount)
            };
        }
    }
}