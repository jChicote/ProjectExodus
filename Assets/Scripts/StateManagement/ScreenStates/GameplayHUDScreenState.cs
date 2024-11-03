using System;
using Codice.CM.Common;
using ProjectExodus.Common.Services;
using ProjectExodus.UserInterface.GameplayHUD;
using ProjectExodus.UserInterface.GameplayHUD.Initializer;
using ProjectExodus.UserInterface.GameplayHUD.Mediator;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class GameplayHUDScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private IGameplayHUDMediator m_Mediator;
        
        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IScreenState.Initialize()
        {
            IServiceLocator _ServiceLocator = GameManager.Instance.ServiceLocator;
            this.m_Mediator = _ServiceLocator.GetService<IGameplayHUDMediator>();
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState()
        {
            // Check whether we are in scene and an active HUD instance exists
            //
            // ICommand _GameplayHUDInitializerCommand = new GameplayHUDInitializerCommand(
            //     Object.FindFirstObjectByType<GameplayHUDView>(FindObjectsInactive.Exclude),
            //     this.m_Mediator,
            //     _ShipAsset,
            //     _ShipToSpawn);
            // _GameplayHUDInitializerCommand.Execute();
        }

        void IScreenState.EndState() 
            => Debug.LogWarning("[WARNING]: Gameplay HUD is not implemented.");

        #endregion Methods
  
    }

}