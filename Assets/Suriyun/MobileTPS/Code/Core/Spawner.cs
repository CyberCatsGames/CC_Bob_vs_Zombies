using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Suriyun.MobileTPS
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Wave> _waves;

        private Coroutine _spawnCoroutine;
        private int _currentWaveIndex;
        private int _killCount;
        private int WaveEnemyCount => _waves[_currentWaveIndex].EnemyCount;

        public event Action WaveFinished;

        public void StartSpawning()
        {
            if (_spawnCoroutine != null)
                return;

            _spawnCoroutine = StartCoroutine(Spawn());
        }

        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }

        private IEnumerator Spawn()
        {
            _killCount = 0;
            _currentWaveIndex %= _waves.Count;

            var wave = _waves[_currentWaveIndex];
            int count = WaveEnemyCount;
            var waitForSeconds = new WaitForSeconds(wave.Cooldown);

            while (count > 0)
            {
                var enemy = Instantiate(wave.GetRandomEnemy(), wave.GetRandomPoint().position, Quaternion.identity);
                enemy.Died += OnEnemyDied;
                count--;
                yield return waitForSeconds;
            }

            _spawnCoroutine = null;
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            _killCount++;
            print($"Score {_killCount} : {WaveEnemyCount}");

            if (WaveEnemyCount == _killCount)
            {
                WaveFinished?.Invoke();
                _currentWaveIndex++;
            }
        }
    }

    [Serializable]
    public class Wave
    {
        [SerializeField] private List<PartWaveSetting> _waveParts;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _cooldown = 1f;

        public float Cooldown => _cooldown;

        public int EnemyCount => _waveParts.Sum(part => part.Count);

        public Enemy GetRandomEnemy()
        {
            var part = _waveParts[Random.Range(0, _waveParts.Count)];
            return part.Template;
        }

        public Transform GetRandomPoint()
        {
            return _spawnPoints[Random.Range(0, _spawnPoints.Count - 1)];
        }
    }

    [Serializable]
    public class PartWaveSetting
    {
        [SerializeField] private Enemy _template;
        [SerializeField] private int _count;

        public Enemy Template => _template;

        public int Count => _count;
    }
}