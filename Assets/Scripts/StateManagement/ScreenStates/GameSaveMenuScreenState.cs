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

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start() 
            => this.m_ScreenController = this.GetComponent<IGameSaveSelectionMenuController>().GetScreenController();

        #endregion Initializers
  
        // #region - - - - - - Controllers - - - - - -
        //
        // public GameSaveMenuScreenState(IGameSaveSelectionMenuController gameSaveSelectionMenuController)
        // {
        //     if (gameSaveSelectionMenuController == null)
        //         throw new ArgumentNullException(nameof(gameSaveSelectionMenuController));
        //     
        //     this.m_ScreenController = gameSaveSelectionMenuController.GetScreenController();
        // }
        //
        // #endregion Controllers
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState()
            => this.m_ScreenController.ShowScreen();

        void IScreenState.EndState()
            => this.m_ScreenController.HideScreen();

        #endregion Methods

    }

}