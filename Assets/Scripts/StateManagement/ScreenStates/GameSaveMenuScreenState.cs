using System;
using ProjectExodus.UserInterface;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class GameSaveMenuScreenState : IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private readonly IScreenStateController m_ScreenController;

        #endregion Fields
  
        #region - - - - - - Controllers - - - - - -

        public GameSaveMenuScreenState(IGameSaveSelectionMenuController gameSaveSelectionMenuController)
        {
            if (gameSaveSelectionMenuController == null)
                throw new ArgumentNullException(nameof(gameSaveSelectionMenuController));
            
            this.m_ScreenController = gameSaveSelectionMenuController.GetScreenController();
        }

        #endregion Controllers
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState()
            => this.m_ScreenController.ShowScreen();

        void IScreenState.EndState()
            => this.m_ScreenController.HideScreen();

        #endregion Methods

    }

}