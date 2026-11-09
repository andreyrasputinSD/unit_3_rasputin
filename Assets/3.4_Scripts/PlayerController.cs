using System.Collections.Generic;
using UnityEngine;

namespace TZ
{
    /// <summary>
    /// Reads player input and forwards it to the gameplay entities
    /// (spawner, cloud, villagers). Input is kept separate from the logic.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public Spawner spawner;
        public CloudController cloudController;
        public List<Refresh> villagers = new List<Refresh>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("PlayerController: X key down (spawn rock)", this);
                if (spawner != null)
                {
                    spawner.Spawn();
                }
                else
                {
                    Debug.LogError("PlayerController: spawner is not assigned", this);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("PlayerController: Z key down (move cloud)", this);
                if (cloudController != null)
                {
                    cloudController.Action();
                }
                else
                {
                    Debug.LogError("PlayerController: cloudController is not assigned", this);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("PlayerController: Space key down (change tools)", this);
                ChangeTools();
            }
        }

        public void ChangeTools()
        {
            foreach (Refresh villager in villagers)
            {
                if (villager != null)
                {
                    villager.ChangeTool();
                }
            }
        }
    }
}
