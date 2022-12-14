using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Suriyun.MobileTPS
{
    public class Game : MonoBehaviour
    {

        public static Game instance;

        public GameObject enemy_prefab;
        public MapData map_data;

        public UnityEvent EventGameStart;
        public UnityEvent EventGameRestart;
        public UnityEvent EventGameOver;

        void Awake()
        {
            instance = this;
            // Perform game setttings here //
            Application.targetFrameRate = 60;
        }

        IEnumerator Spawner()
        {
            while (true)
            {
                int rand = UnityEngine.Random.Range(0, map_data.enemy_spawn_point.Count - 1);
                GameObject g = (GameObject)Instantiate(enemy_prefab);
                g.transform.parent = null;
                g.transform.position = map_data.enemy_spawn_point[rand].position;
                yield return new WaitForSecondsRealtime(1f);
            }
        }


        public void GameStart()
        {
            EventGameStart.Invoke();
            StartCoroutine(Spawner());
        }

        public void GameRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ShowGameOverMenu()
        {
            EventGameOver.Invoke();
            Debug.Log("Game Over");
            StopCoroutine(Spawner());
        }

    }

    [Serializable]
    public class MapData
    {
        // Destinations data for actor navigation
        // Player is in Red team so it will only move along red_move_pos
        public List<Transform> red_move_pos;
        public List<Transform> blue_move_pos;
        public List<Transform> enemy_spawn_point;
    }
}