using System;
using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.UserInterface.Controllers;
using UnityEngine;

namespace ProjectExodus.UserInterface.PauseScreen
{

    public class PauseScreenPresenter : MonoBehaviour, IPauseScreenPresenter, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        private IGameStateManager m_GameStateManager;
        private IPauseController m_PauseController;
        private PauseScreenView m_View;
        private IUserInterfaceController m_UserInterfaceController;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IPauseScreenPresenter.Initialize(
            IGameStateManager gameStateManager,
            IPauseController pauseController, 
            IUserInterfaceController userInterfaceController)
        {
            this.m_GameStateManager = gameStateManager ?? throw new ArgumentNullException(nameof(gameStateManager));
            this.m_PauseController = pauseController ?? throw new ArgumentNullException(nameof(pauseController));
            this.m_UserInterfaceController =
                userInterfaceController ?? throw new ArgumentNullException(nameof(userInterfaceController));
            
            this.m_View = this.GetComponent<PauseScreenView>();
            this.BindMethodsToView();
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen() 
            => this.m_View.HideScreen();

        void IScreenStateController.ShowScreen() 
            => this.m_View.ShowScreen();

        private void BindMethodsToView()
        {
            this.m_View.ResumeButton.onClick.AddListener(this.ResumeGame);
            this.m_View.OptionsButton.onClick.AddListener(this.OpenOptionsMenu);
            this.m_View.ExitButton.onClick.AddListener(this.ExitGameplay);
        }

        private void ResumeGame()
        {
            this.m_UserInterfaceController.OpenScreen(UIScreenType.GameplayHUD);
            this.m_PauseController.UnPause();
        }

        private void OpenOptionsMenu() 
            => this.m_UserInterfaceController.OpenScreen(UIScreenType.OptionsMenu);

        private void ExitGameplay() 
            => this.m_GameStateManager.ChangeGameState(GameState.MainMenu);

        #endregion Methods

    }

}