using TMPro;
using UnityEngine;

namespace Golf
{
    /// <summary>
    /// Состояние геймплея: включает игрока и уровень, показывает очки
    /// и переводит игру в состояние Game Over.
    /// </summary>
    public class GamePlayState : GameState
    {
        public LevelController levelController;
        public PlayerController playerController;
        public GameState gameOverState;
        public TMP_Text scoreText;

        private bool m_isGameOver;

        public void OnGameOver()
        {
            if (m_isGameOver)
            {
                return;
            }

            m_isGameOver = true;

            // По ТЗ в конце игры все камни удаляются со сцены.
            if (levelController != null)
            {
                levelController.ClearStones();
            }

            Exit();
            gameOverState.Enter();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            m_isGameOver = false;

            if (levelController != null)
            {
                // LevelController подписывается на onStickHit раньше нас,
                // поэтому к моменту обновления текста счёт уже увеличен.
                levelController.enabled = true;
            }

            if (playerController != null)
            {
                playerController.enabled = true;
            }

            GameEvents.onCollisionStones += OnGameOver;
            GameEvents.onStickHit += OnStickHit;

            OnStickHit();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            GameEvents.onCollisionStones -= OnGameOver;
            GameEvents.onStickHit -= OnStickHit;

            if (levelController != null)
            {
                levelController.enabled = false;
            }

            if (playerController != null)
            {
                playerController.enabled = false;
            }
        }

        private void OnStickHit()
        {
            if (scoreText != null && levelController != null)
            {
                scoreText.text = $"Score : {levelController.score}";
            }
        }
    }
}
