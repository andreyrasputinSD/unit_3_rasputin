using System.Collections.Generic;
using UnityEngine;

namespace TZ
{
    /// <summary>
    /// Spawns a random rock from the list at the spawner position.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        public List<GameObject> prefabs = new List<GameObject>();

        public void Spawn()
        {
            if (prefabs == null || prefabs.Count == 0)
            {
                Debug.LogError("Spawner: prefabs list is empty", this);
                return;
            }

            GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
            if (prefab == null)
            {
                Debug.LogError("Spawner: prefab == null", this);
                return;
            }

            Instantiate(prefab, transform.position, Quaternion.identity);
            Debug.Log($"Spawner: spawned {prefab.name}", this);
        }
    }
}
