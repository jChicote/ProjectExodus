using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;

namespace ProjectExodus.UserInterface.ScreenStates
{

    public class GameplayHUDScreenState : IScreenState
    {

        #region - - - - - - Constructors - - - - - -

        public GameplayHUDScreenState(IGameplayHUDController gameplayHUDController)
        {
            
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => Debug.LogWarning("[WARNING]: Gameplay HUD is not implemented.");

        void IScreenState.EndState() 
            => Debug.LogWarning("[WARNING]: Gameplay HUD is not implemented.");

        #endregion Methods
  
    }

}