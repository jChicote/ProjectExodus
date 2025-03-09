using System.Collections;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.Movement
{

    public class PlayerMovement : PausableMonoBehavior, IPlayerMovement
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private Rigidbody2D m_Rigidbody;

        [SerializeField] private float m_MaxThrustMagnitude;
        [SerializeField] private float m_MoveInterpolationInterval;
        [SerializeField] private float m_ThrustPower;
        [SerializeField] private float m_AfterburnPower;
        [SerializeField] private float m_AfterburnTimeLength;

        private Vector2 m_MoveDirection;
        private float m_TotalSpeed;
        
        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start() 
            => this.m_TotalSpeed = this.m_ThrustPower;

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
            this.StartCoroutine(this.RunAfterBurn());
            Debug.Log("Thrust power toggled.");
        }

        private IEnumerator RunAfterBurn()
        {
            float _SpeedBefore = this.m_TotalSpeed;
            float _TargetAfterburnSpeed = this.m_TotalSpeed + this.m_AfterburnPower;
            float _AfterburnRuntime = 0;
            
            while (_AfterburnRuntime < this.m_AfterburnTimeLength)
            {
                this.m_TotalSpeed = Mathf.Lerp(
                    _SpeedBefore,
                    _TargetAfterburnSpeed,
                    _AfterburnRuntime / this.m_AfterburnTimeLength);

                _AfterburnRuntime += Time.deltaTime;
            }

            yield return null;
        }

        private void RunMovement()
        {
            if (this.m_Rigidbody.linearVelocity.sqrMagnitude > this.m_MaxThrustMagnitude)
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