using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using UnityEngine;

namespace ProjectExodus.UserInterface.MainMenu
{

    public class MainMenuController : MonoBehaviour, IMainMenuController
    {

        #region - - - - - - Fields - - - - - -

        [Header("Views")] 
        [SerializeField] private GameObject m_ContentGroup;

        private IGameStateManager m_GameStateManager;
        private IUserInterfaceScreenStateManager m_UserInterfaceScreenStateManager;

        #endregion Fields

        #region - - - - - - Events - - - - - -

        public void OnPlaySelection()
        {
            this.m_GameStateManager.ChangeGameState(GameState.Gameplay);
            this.m_UserInterfaceScreenStateManager.OpenMenu(UIScreenType.GameplayHUD);
        }

        public void OnOptionsSelection() 
            => this.m_UserInterfaceScreenStateManager.OpenMenu(UIScreenType.OptionsMenu);

        public void OnExitSelection()
        {
            Debug.LogWarning("[MainMenuController] >> Exit game.");
            Application.Quit();
        }

        #endregion Events

        #region - - - - - - Methods - - - - - -

        void IMainMenuController.InitialiseMainMenuController()
        {
            this.m_GameStateManager = GameManager.Instance.GameStateManager;
            this.m_UserInterfaceScreenStateManager =
                GameManager.Instance.UserInterfaceManager.UserInterfaceScreenStateManager;
        }

        void IScreenStateController.HideScreen()
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods

    }

}