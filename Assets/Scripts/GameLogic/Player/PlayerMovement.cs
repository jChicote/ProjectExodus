using System;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player
{

    public class PlayerMovement : MonoBehaviour, IPlayerMovement
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private float m_ThrustPower;

        private Vector2 m_MoveDirection;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Update()
        {
            
        }

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Methods - - - - - -

        void IPlayerMovement.SetLookDirection(Vector2 lookDirection) 
            => this.transform.rotation =
                Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90));

        void IPlayerMovement.SetMoveDirection(Vector2 moveDirection)
            => this.m_MoveDirection = moveDirection;

        void IPlayerMovement.ToggleAfterburn()
        {
            Debug.Log("Thrust power toggled.");
        }

        private void RunMovement()
        {
            
        }

        #endregion Methods
  
    }

}