using ProjectExodus.Common.Services;
using ProjectExodus.UserInterface;
using ProjectExodus.UserInterface.GameplayHUD.Initializer;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class GameplayHUDScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private IScreenStateController m_ScreenController;
        
        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IScreenState.Initialize()
        {
            this.m_ScreenController = this.GetComponent<IScreenStateController>();
            
            ICommand _GameplayHUDInitializerCommand = new GameplayHUDInitializerCommand(this.gameObject);
            _GameplayHUDInitializerCommand.Execute();
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_ScreenController.ShowScreen();

        void IScreenState.EndState()
            => this.m_ScreenController.HideScreen();

        #endregion Methods

    }

}