using UnityEngine;
using UnityEngine.EventSystems;

namespace Golf
{
    /// <summary>
    /// Прямое управление игроком: кнопка Hit (Event Trigger) вызывает OnDown/OnUp.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player player;

        public void OnDown()
        {
            if (player != null)
            {
                player.SetDown(true);
            }
        }

        public void OnUp()
        {
            if (player != null)
            {
                player.SetDown(false);
            }
        }

        // Перегрузки для Event Trigger (PointerDown / PointerUp передают BaseEventData).
        public void OnDown(BaseEventData data)
        {
            OnDown();
        }

        public void OnUp(BaseEventData data)
        {
            OnUp();
        }
    }
}
