using System.Collections.Generic;
using TZ;
using UnityEngine;

namespace Golf
{
    /// <summary>
    /// Управляет уровнем: спавнит камни со случайной задержкой, считает очки
    /// и умеет очищать сцену от камней.
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        public Spawner spawner;
        public int score = 0;
        public int hightScore = 0;

        // Настройки появления берём из префаба камня (ТЗ, п. 3).
        private float m_delayMin = 0.5f;
        private float m_delayMax = 2f;
        private float m_delayStep = 0.1f;
        private float m_delay;
        private float m_lastSpawnedTime;

        private readonly List<GameObject> m_stones = new List<GameObject>();

        private void OnEnable()
        {
            GameEvents.onStickHit += OnStickHit;

            score = 0;
            ResetSpawnTiming();
            m_lastSpawnedTime = Time.time;
        }

        private void OnDisable()
        {
            GameEvents.onStickHit -= OnStickHit;
        }

        private void Update()
        {
            if (Time.time < m_lastSpawnedTime + m_delay)
            {
                return;
            }

            SpawnStone();
        }

        private void SpawnStone()
        {
            if (spawner == null)
            {
                return;
            }

            GameObject stone = spawner.Spawn();
            if (stone != null)
            {
                m_stones.Add(stone);
            }

            m_lastSpawnedTime = Time.time;
            RefreshDelay();
        }

        /// <summary>
        /// Читает границы паузы из префаба камня и сбрасывает прогрессию.
        /// </summary>
        public void ResetSpawnTiming()
        {
            m_delayMin = 0.5f;
            m_delayMax = 2f;
            m_delayStep = 0.1f;

            if (spawner != null)
            {
                GameObject prefab = spawner.GetRandomPrefab();
                Stone config = prefab != null ? prefab.GetComponent<Stone>() : null;

                if (config != null)
                {
                    m_delayMin = config.spawnDelayMin;
                    m_delayMax = config.spawnDelayMax;
                    m_delayStep = config.spawnDelayStep;
                }
            }

            RefreshDelay();
        }

        /// <summary>
        /// Считает паузу до следующего камня и уменьшает верхнюю границу
        /// на delayStep (по ТЗ каждый следующий камень падает чаще).
        /// </summary>
        public void RefreshDelay()
        {
            m_delay = Random.Range(m_delayMin, Mathf.Max(m_delayMin, m_delayMax));
            m_delayMax = Mathf.Max(m_delayMin, m_delayMax - m_delayStep);
        }

        private void OnStickHit()
        {
            score++;
            hightScore = Mathf.Max(hightScore, score);
        }

        /// <summary>
        /// Удаляет все камни со сцены.
        /// </summary>
        public void ClearStones()
        {
            foreach (GameObject stone in m_stones)
            {
                if (stone != null)
                {
                    Destroy(stone);
                }
            }

            m_stones.Clear();

            // Страховка: убираем камни, которые попали в сцену не через спавнер.
            foreach (Stone stone in FindObjectsOfType<Stone>())
            {
                if (stone != null)
                {
                    Destroy(stone.gameObject);
                }
            }
        }
    }
}
