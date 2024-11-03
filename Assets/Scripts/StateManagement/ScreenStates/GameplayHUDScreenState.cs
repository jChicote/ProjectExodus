using System;
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

        #region - - - - - - Initializers - - - - - -

        void IScreenState.Initialize()
        {
            throw new NotImplementedException();
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState()
        {
            // ICommand _GameplayHUDInitializerCommand = new GameplayHUDInitializerCommand(
            //     Object.FindFirstObjectByType<GameplayHUDView>(FindObjectsInactive.Exclude),
            //     initializationContext.ServiceLocator.GetService<IGameplayHUDMediator>(),
            //     _ShipAsset,
            //     _ShipToSpawn);
            // _GameplayHUDInitializerCommand.Execute();
        }

        void IScreenState.EndState() 
            => Debug.LogWarning("[WARNING]: Gameplay HUD is not implemented.");

        #endregion Methods
  
    }

}