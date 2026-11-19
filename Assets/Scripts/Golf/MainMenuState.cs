using TMPro;
using UnityEngine;

namespace Golf
{
    /// <summary>
    /// Главное меню: показывает лучший счёт и запускает игру по кнопке Play.
    /// </summary>
    public class MainMenuState : GameState
    {
        public LevelController levelController;
        public GameState gamePlayState;
        public TMP_Text scoreText;

        public void PlayGame()
        {
            Exit();
            gamePlayState.Enter();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (scoreText != null && levelController != null)
            {
                scoreText.text = $"Score : {levelController.hightScore}";
            }
        }
    }
}
