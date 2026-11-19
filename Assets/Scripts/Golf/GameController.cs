using UnityEngine;

namespace Golf
{
    /// <summary>
    /// Точка входа в игру: на старте включает главное меню,
    /// дальше состоянием управляют сами стейты.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public MainMenuState mainMenuState;

        private void Start()
        {
            if (mainMenuState == null)
            {
                Debug.LogError("GameController: mainMenuState is not assigned", this);
                return;
            }

            mainMenuState.Enter();
        }
    }
}
