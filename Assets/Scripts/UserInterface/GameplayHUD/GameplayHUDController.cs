using System;
using ProjectExodus.UserInterface.GameplayHUD.Mediator;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDController : MonoBehaviour, IGameplayHUDController, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        private IGameplayHUDMediator m_GameplayHUDMediator;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IGameplayHUDController.Initialize(IGameplayHUDMediator gameplayHUDMediator)
        {
            this.m_GameplayHUDMediator =
                gameplayHUDMediator ?? throw new ArgumentNullException(nameof(gameplayHUDMediator));
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen()
        {
            throw new NotImplementedException();
        }

        void IScreenStateController.ShowScreen()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
  
    }

}