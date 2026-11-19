using UnityEngine;

namespace Golf
{
    /// <summary>
    /// Экран Game Over: по кнопке Restart чистит камни и возвращает в главное меню.
    /// </summary>
    public class GameOverState : GameState
    {
        public GameState mainMenuState;
        public LevelController levelController;

        public void Restart()
        {
            if (levelController != null)
            {
                levelController.ClearStones();
            }

            Exit();
            mainMenuState.Enter();
        }
    }
}
