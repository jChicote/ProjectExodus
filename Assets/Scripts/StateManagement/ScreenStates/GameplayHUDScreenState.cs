using System;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;

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
            => Debug.LogWarning("[WARNING]: Gameplay HUD is not implemented.");

        void IScreenState.EndState() 
            => Debug.LogWarning("[WARNING]: Gameplay HUD is not implemented.");

        #endregion Methods
  
    }

}