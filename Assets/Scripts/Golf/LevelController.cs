using System.Collections;
using TZ;
using UnityEngine;

namespace Golf
{
    /// <summary>
    /// Управляет уровнем: запускает падение камней и следит за концом игры.
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        public Spawner spawner;
        public bool isGameOver = false;
        public float delay = 0.5f;

        private void Start()
        {
            StartCoroutine(SpawnStoneProc());
        }

        private IEnumerator SpawnStoneProc()
        {
            do
            {
                yield return new WaitForSeconds(delay);

                if (spawner != null)
                {
                    spawner.Spawn();
                }
                else
                {
                    Debug.LogError("LevelController: spawner is not assigned", this);
                }
            }
            while (!isGameOver);
        }
    }
}
