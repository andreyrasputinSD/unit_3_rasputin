using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    /// <summary>
    /// Базовое состояние игры: включает и выключает свои View-объекты,
    /// а также умеет входить (Enter) и выходить (Exit).
    /// </summary>
    public abstract class GameState : MonoBehaviour
    {
        public List<GameObject> views = new List<GameObject>();

        public virtual void Enter()
        {
            gameObject.SetActive(true);
        }

        public virtual void Exit()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnEnable()
        {
            SetViewsActive(true);
        }

        protected virtual void OnDisable()
        {
            SetViewsActive(false);
        }

        private void SetViewsActive(bool value)
        {
            if (views == null)
            {
                return;
            }

            foreach (GameObject item in views)
            {
                if (item)
                {
                    item.SetActive(value);
                }
            }
        }
    }
}
