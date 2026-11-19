using UnityEngine;

namespace Golf
{
    /// <summary>
    /// Камень: сообщает через GameEvents, что столкнулись два нетронутых камня,
    /// и помнит, был ли он уже отбит мотыгой.
    /// </summary>
    public class Stone : MonoBehaviour
    {
        public bool isAfect = false;

        [Header("Настройки появления (ТЗ, п. 3)")]
        [Tooltip("Минимальная пауза перед появлением следующего камня, сек.")]
        public float spawnDelayMin = 0.5f;

        [Tooltip("Максимальная пауза перед появлением следующего камня, сек.")]
        public float spawnDelayMax = 2f;

        [Tooltip("Шаг, на который уменьшается верхняя граница паузы, сек.")]
        public float spawnDelayStep = 0.1f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision == null)
            {
                return;
            }

            if (!collision.transform.TryGetComponent(out Stone other))
            {
                return;
            }

            // По ТЗ игру заканчивает только столкновение двух камней,
            // по которым ещё не били мотыгой.
            if (isAfect || other.isAfect)
            {
                return;
            }

            GameEvents.CollisionStonesInvoke(collision);
        }
    }
}
