using System;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.StateManagement.ScreenStates;
using ProjectExodus.UserInterface.Controllers;
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
        private IUserInterfaceController m_UserInterfaceController;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IMainMenuController.InitialiseMainMenuController(
            IGameStateManager gameStateManager, 
            IUserInterfaceController userInterfaceController)
        {
            this.m_GameStateManager = gameStateManager ?? 
                                      throw new ArgumentNullException(nameof(gameStateManager));
            this.m_UserInterfaceController = userInterfaceController ??
                                             throw new ArgumentNullException(nameof(userInterfaceController));
        }

        #endregion Initializers
  
        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_PlayButton.onClick.AddListener(this.PlaySelectionTriggered);
            this.m_OptionsButton.onClick.AddListener(this.OptionsSelectionTriggered);
            this.m_ExitButton.onClick.AddListener(this.ExitSelectionTriggered);

            IScreenState _MainMenuScreenState = this.GetComponent<IScreenState>();
            _MainMenuScreenState.Initialize();
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen()
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);
        
        private void PlaySelectionTriggered() 
            => this.m_GameStateManager.ChangeGameState(GameState.Gameplay);

        private void OptionsSelectionTriggered() 
            => this.m_UserInterfaceController.OpenScreen(UIScreenType.OptionsMenu);

        private void ExitSelectionTriggered()
        {
            Debug.LogWarning("[MainMenuController] >> Exit game.");
            Application.Quit();
        }

        #endregion Methods

    }

}