using UnityEngine;

namespace Golf
{
    /// <summary>
    /// Игрок: машет клюшкой, когда PlayerController сообщает о нажатии/отпускании,
    /// и толкает камни, с которыми столкнулась клюшка.
    /// </summary>
    public class Player : MonoBehaviour
    {
        public Transform stick;
        public Transform help;
        public float range = 30f;
        public float power = 20f;

        private bool m_isDown;
        private Vector3 m_lastPosition;
        private Stick m_stick;

        public void SetDown(bool value)
        {
            m_isDown = value;

            if (value)
            {
                WakeStonesNearStrikeZone();
            }
        }

        /// <summary>
        /// Будит спящие камни у зоны удара: спящий Rigidbody не реагирует
        /// на кинематическую мотыгу, которой двигают трансформ.
        /// </summary>
        private void WakeStonesNearStrikeZone()
        {
            if (help == null)
            {
                return;
            }

            Vector3 center = help.position;

            foreach (Stone stone in FindObjectsOfType<Stone>())
            {
                Rigidbody body = stone.GetComponent<Rigidbody>();
                if (body != null && (stone.transform.position - center).sqrMagnitude < 9f)
                {
                    body.WakeUp();
                }
            }
        }

        private void Awake()
        {
            if (stick != null)
            {
                m_stick = stick.GetComponentInChildren<Stick>();
            }
        }

        private void Update()
        {
            if (stick == null)
            {
                return;
            }

            // Позиция ударной части на начало кадра — по ней считаем направление маха.
            if (help != null)
            {
                m_lastPosition = help.position;
            }

            // По ТЗ скорость маха настраивается в префабе мотыги (компонент Stick).
            float speed = m_stick != null ? m_stick.speed : 500f;

            Quaternion toRot = Quaternion.Euler(0f, 0f, m_isDown ? range : -range);
            stick.localRotation = Quaternion.RotateTowards(stick.localRotation, toRot, speed * Time.deltaTime);
        }

        /// <summary>
        /// Вызывается клюшкой при столкновении с объектом, у которого есть Collider.
        /// </summary>
        public void OnCollisionStick(Collider collider)
        {
            if (collider == null)
            {
                return;
            }

            if (!collider.TryGetComponent(out Rigidbody body))
            {
                return;
            }

            // Направление удара — смещение ударной части за последний кадр.
            Vector3 dir = help != null ? help.position - m_lastPosition : stick.right;

            if (dir.sqrMagnitude < 0.000001f)
            {
                return;
            }

            dir.Normalize();
            body.AddForce(dir * power, ForceMode.Impulse);

            // За каждое касание камня мотыгой начисляем 1 балл (ТЗ, п. 4).
            // Камень помечаем как отбитый — он больше не может завершить игру.
            if (collider.TryGetComponent(out Stone stone))
            {
                stone.isAfect = true;
                GameEvents.StickHit();
            }
        }
    }
}
