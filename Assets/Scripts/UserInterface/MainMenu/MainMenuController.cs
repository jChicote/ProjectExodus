using ProjectExodus.Management.GameStateManager;
using UnityEngine;

namespace ProjectExodus.UserInterface.MainMenu
{

    public class MainMenuController : MonoBehaviour, IMainMenuController
    {

        #region - - - - - - Fields - - - - - -

        [Header("Views")] [SerializeField] private GameObject m_ContentGroup;

        private IGameStateManager m_GameStateManager;

        #endregion Fields

        #region - - - - - - Events - - - - - -

        public void OnPlaySelection()
        {
            this.m_GameStateManager.ChangeGameState(GameState.Gameplay);
            ((IMainMenuController)this).HideMainMenu();
        }

        public void OnOptionsSelection()
            => Debug.LogWarning("[WARNING] >> No implemented behavior.");

        public void OnExitSelection()
        {
            Debug.LogWarning("[MainMenuController] >> Exit game.");
            Application.Quit();
        }

        #endregion Events

        #region - - - - - - Methods - - - - - -

        void IMainMenuController.InitialiseMainMenuController() 
            => this.m_GameStateManager = GameManager.Instance.GameStateManager;

        void IMainMenuController.HideMainMenu()
            => this.m_ContentGroup.SetActive(false);

        void IMainMenuController.ShowMainMenu() 
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods

    }

}