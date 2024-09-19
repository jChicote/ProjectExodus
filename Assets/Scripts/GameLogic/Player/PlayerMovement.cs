using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player
{

    public class PlayerMovement : PausableMonoBehavior, IPlayerMovement
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private Rigidbody2D m_Rigidbody;

        [SerializeField] private float m_MaxThrustMagnitude;
        [SerializeField] private float m_MoveInterpolationInterval;
        [SerializeField] private float m_ThrustPower;

        private Vector2 m_MoveDirection;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Update()
        {
            if (this.m_IsPaused) return;
            
            this.RunMovement();
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
            float _MovementMagnitude = this.m_Rigidbody.linearVelocity.magnitude; // This is inefficient (use squared magnitude)
            Debug.Log($"[LOG] Movement Magnitude: {_MovementMagnitude}");

            if (_MovementMagnitude > this.m_MaxThrustMagnitude)
            {
                this.m_Rigidbody.linearVelocity = Vector2.Lerp(
                    this.m_Rigidbody.linearVelocity, 
                    Vector2.zero,
                    this.m_MoveInterpolationInterval);
                
                return;
            }
            
            this.m_Rigidbody.linearVelocity = Vector2.Lerp(
                this.m_Rigidbody.linearVelocity, 
                this.m_MoveDirection * this.m_ThrustPower, 
                this.m_MoveInterpolationInterval);
        }

        #endregion Methods
  
    }

}