using ProjectExodus.UserInterface;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class GameSaveMenuScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private IScreenStateController m_ScreenController;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IScreenState.Initialize()
            => this.m_ScreenController = this.GetComponent<IGameSaveSelectionMenuController>().GetScreenController();

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState()
            => this.m_ScreenController.ShowScreen();

        void IScreenState.EndState()
            => this.m_ScreenController.HideScreen();
        
        object IScreenState.GetInterfaceController()
            => this.GetComponent<IGameSaveSelectionMenuController>();

        #endregion Methods

    }

}