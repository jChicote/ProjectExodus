﻿using ProjectExodus.Common.Services;
using UnityEngine.InputSystem;

namespace ProjectExodus.GameLogic.Input.Gameplay
{

    public interface IGameplayInputControl : IInputControl
    {

        #region - - - - - - Methods - - - - - -

        void InitializeGameplayInputControl(ICommand initializerCommand);

        void OnAttack(InputAction.CallbackContext callback);

        void OnAttackRelease(InputAction.CallbackContext callback);

        void OnInteract(InputAction.CallbackContext callbackContext);
        
        void OnLook(InputAction.CallbackContext callback);

        void OnMove(InputAction.CallbackContext callback);

        void OnPause(InputAction.CallbackContext callback);

        void OnSprint(InputAction.CallbackContext callbackContext);

        void SetServiceContainer(GameplayInputControlServiceContainer serviceContainer);

        #endregion Methods

    }

}