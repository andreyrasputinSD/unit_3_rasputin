using UnityEngine;
using UnityEngine.Events;

namespace Golf
{
    /// <summary>
    /// Мотыга: ловит столкновения своим Mesh Collider-ом и пробрасывает их
    /// наружу через UnityEvent, чтобы игрок мог на них подписаться.
    /// </summary>
    public class Stick : MonoBehaviour
    {
        public UnityEvent<Collider> onCollision = new UnityEvent<Collider>();

        [Header("Настройки префаба мотыги (ТЗ, п. 2)")]
        [Tooltip("Скорость прохождения траектории мотыгой от точки A до B и обратно, град/сек.")]
        public float speed = 500f;

        private void Awake()
        {
            // Быстрый мах тонкой мотыгой иначе проскакивает сквозь камень.
            Rigidbody body = GetComponent<Rigidbody>();
            if (body != null && body.isKinematic)
            {
                body.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision == null)
            {
                return;
            }

            onCollision?.Invoke(collision.collider);
        }
    }
}
