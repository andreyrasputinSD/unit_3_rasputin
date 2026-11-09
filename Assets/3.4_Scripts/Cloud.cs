using UnityEngine;

namespace TZ
{
    /// <summary>
    /// Holds a reference to the rain particle system and exposes Play/Stop helpers.
    /// </summary>
    public class Cloud : MonoBehaviour
    {
        [SerializeField] private ParticleSystem m_particleSystem;

        public void PlayFX()
        {
            if (m_particleSystem == null)
            {
                Debug.LogError("Cloud: particle system is not assigned", this);
                return;
            }
            m_particleSystem.Play();
        }

        public void StopFX()
        {
            if (m_particleSystem == null)
            {
                Debug.LogError("Cloud: particle system is not assigned", this);
                return;
            }
            m_particleSystem.Stop();
        }

        public void Start()
        {
            if (m_particleSystem != null)
            {
                m_particleSystem.Stop();
            }
        }
    }
}
