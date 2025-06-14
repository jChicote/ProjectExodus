using UnityEngine;

namespace ProjectExodus.GameLogic.Player.Movement
{

    public interface IPlayerMovement
    {

        #region - - - - - - Methods - - - - - -
        
        void SetLookDirection(Vector2 lookDirection);

        void SetMoveDirection(Vector2 moveDirection);

        void StartAfterburn();

        void EndAfterburn();

        #endregion Methods

    }

}