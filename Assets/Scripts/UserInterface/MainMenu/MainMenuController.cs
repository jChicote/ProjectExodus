using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.MainMenu
{

    public class MainMenuController : MonoBehaviour, IMainMenuController
    {

        #region - - - - - - Fields - - - - - -

        [Header("Views")] 
        [SerializeField] private GameObject m_ContentGroup;
        [SerializeField] private Button m_PlayButton;
        [SerializeField] private Button m_OptionsButton;
        [SerializeField] private Button m_ExitButton;

        private IGameStateManager m_GameStateManager;
        private IUserInterfaceScreenStateManager m_UserInterfaceScreenStateManager;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_PlayButton.onClick.AddListener(this.OnPlaySelection);
            this.m_OptionsButton.onClick.AddListener(this.OnOptionsSelection);
            this.m_ExitButton.onClick.AddListener(this.OnExitSelection);
        }

        #endregion Unity Methods
  
        #region - - - - - - Events - - - - - -

        public void OnPlaySelection()
        {
            this.m_GameStateManager.ChangeGameState(GameState.Gameplay);
            this.m_UserInterfaceScreenStateManager.OpenScreen(UIScreenType.GameplayHUD);
        }

        public void OnOptionsSelection() 
            => this.m_UserInterfaceScreenStateManager.OpenScreen(UIScreenType.OptionsMenu);

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