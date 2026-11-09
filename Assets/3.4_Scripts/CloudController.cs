using UnityEngine;

namespace TZ
{
    /// <summary>
    /// Moves the cloud between the target points. The cloud rains while it stands
    /// above a character and the rain is stopped while the cloud is flying.
    /// </summary>
    public class CloudController : MonoBehaviour
    {
        private int m_targetIndex = 0;
        private bool m_moved = false;

        public float moveSpeed = 12f;
        public Cloud cloud;
        public Transform[] targets;

        public void Action()
        {
            Debug.Log("CloudController: action", this);

            if (m_moved)
            {
                return;
            }

            if (cloud == null)
            {
                Debug.LogError("CloudController: cloud is not assigned", this);
                return;
            }

            if (targets == null || targets.Length == 0)
            {
                Debug.LogError("CloudController: targets list is empty", this);
                return;
            }

            // Rain stops while the cloud is moving.
            cloud.StopFX();

            m_targetIndex++;
            if (m_targetIndex >= targets.Length)
            {
                m_targetIndex = 0;
            }

            m_moved = true;
        }

        public void Update()
        {
            if (!m_moved || cloud == null || targets == null || targets.Length == 0)
            {
                return;
            }

            Transform target = targets[m_targetIndex];
            if (target == null)
            {
                m_moved = false;
                return;
            }

            Vector3 targetPosition = new Vector3(
                target.position.x,
                cloud.transform.position.y,
                target.position.z);

            Vector3 offset = (targetPosition - cloud.transform.position).normalized
                             * Time.deltaTime * moveSpeed;

            if (Vector3.Distance(cloud.transform.position, targetPosition) < 0.1f)
            {
                cloud.transform.position = targetPosition;
                m_moved = false;
                cloud.PlayFX();
                Debug.Log($"CloudController: reached {target.name}", this);
            }
            else
            {
                cloud.transform.Translate(offset, Space.World);
            }
        }
    }
}
