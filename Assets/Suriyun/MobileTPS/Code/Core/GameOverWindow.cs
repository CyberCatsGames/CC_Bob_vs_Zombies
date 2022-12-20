using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button _restartThisWave;
        [SerializeField] private UnityEngine.UI.Button _restartGame;

        private void OnEnable()
        {
            _restartGame.onClick.AddListener(OnRestartGame);
            _restartThisWave.onClick.AddListener(OnRestartThisWave);
        }

        private void OnDisable()
        {
            _restartGame.onClick.RemoveListener(OnRestartGame);
            _restartThisWave.onClick.RemoveListener(OnRestartThisWave);
        }

        private static void OnRestartThisWave()
        {
            var currentWaveIndex = GameSession.Instance.PlayerInfo.CurrentWaveIndex;
            currentWaveIndex--;
            currentWaveIndex = Mathf.Max(currentWaveIndex, 0);
            GameSession.Instance.PlayerInfo.CurrentWaveIndex = currentWaveIndex;
            Game.instance.GameRestart();
        }

        private static void OnRestartGame()
        {
            Game.instance.RestartAllGame();
        }
    }
}