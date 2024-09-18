using UnityEngine;

namespace ProjectExodus.GameLogic.Player
{

    public class PlayerMovement : MonoBehaviour, IPlayerMovement
    {

        #region - - - - - - Methods - - - - - -

        void IPlayerMovement.SetLookDirection(Vector2 lookDirection)
        {
            Debug.Log($"[LOG]: Successful player look rotation with value at: {lookDirection}");
            this.transform.rotation =  Quaternion.Euler(new Vector3(
                0, 
                0, 
                Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90));
        }

        void IPlayerMovement.SetMoveDirection(Vector2 moveDirection)
        {
            Debug.Log($"[LOG]: Successful player move Direction with value at: {moveDirection}");
        }

        void IPlayerMovement.ToggleAfterburn()
        {
            Debug.Log("Thrust power toggled.");
        }

        #endregion Methods
  
    }

}