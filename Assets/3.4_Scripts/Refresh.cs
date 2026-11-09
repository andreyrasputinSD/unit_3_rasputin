using System.Collections.Generic;
using UnityEngine;

namespace TZ
{
    /// <summary>
    /// Switches the tool in the villager's hand: only the selected tool is active.
    /// </summary>
    public class Refresh : MonoBehaviour
    {
        public List<GameObject> tools = new List<GameObject>();

        private int m_currentIndex = 0;

        public int CurrentIndex => m_currentIndex;

        private void Start()
        {
            // Default tool on start.
            ChangeTool(0);
        }

        /// <summary>Picks a random tool.</summary>
        public void ChangeTool()
        {
            if (tools == null || tools.Count == 0)
            {
                Debug.LogError("Refresh: tools list is empty", this);
                return;
            }
            ChangeTool(Random.Range(0, tools.Count));
        }

        /// <summary>Enables only the tool under the given index.</summary>
        public void ChangeTool(int index)
        {
            if (tools == null || tools.Count == 0)
            {
                Debug.LogError("Refresh: tools list is empty", this);
                return;
            }

            index = Mathf.Clamp(index, 0, tools.Count - 1);
            m_currentIndex = index;

            for (int i = 0; i < tools.Count; i++)
            {
                if (tools[i] != null)
                {
                    tools[i].SetActive(i == index);
                }
            }

            Debug.Log($"Refresh: tool = {tools[index].name}", this);
        }
    }
}
