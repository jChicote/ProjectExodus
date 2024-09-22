using UnityEngine;

namespace ProjectExodus.GameLogic.Player
{

    public interface IPlayerMovement
    {

        #region - - - - - - Methods - - - - - -
        
        void SetLookDirection(Vector2 lookDirection);

        void SetMoveDirection(Vector2 moveDirection);

        void ToggleAfterburn();

        #endregion Methods

    }

}