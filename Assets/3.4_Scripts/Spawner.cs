using System.Collections.Generic;
using UnityEngine;

namespace TZ
{
    /// <summary>
    /// Спавнит случайный камень из списка в позиции спавнера.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        public List<GameObject> prefabs = new List<GameObject>();

        /// <summary>
        /// Возвращает случайный префаб камня (null, если список пуст).
        /// </summary>
        public GameObject GetRandomPrefab()
        {
            if (prefabs == null || prefabs.Count == 0)
            {
                Debug.LogError("Spawner: prefabs list is empty", this);
                return null;
            }

            return prefabs[Random.Range(0, prefabs.Count)];
        }

        public GameObject Spawn()
        {
            return Spawn(GetRandomPrefab());
        }

        public GameObject Spawn(GameObject prefab)
        {
            if (prefab == null)
            {
                Debug.LogError("Spawner: prefab == null", this);
                return null;
            }

            return Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
