using System.Collections;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.Movement
{

    public class PlayerMovement : PausableMonoBehavior, IPlayerMovement
    {

        #region - - - - - - Fields - - - - - -

        // Required Components Fields
        [SerializeField] private Rigidbody2D m_Rigidbody;

        // Thrust Fields
        [SerializeField] private float m_MaxThrustMagnitude;
        [SerializeField] private float m_ThrustPower;
        private float m_TotalSpeed;
        
        // Afterburn Fields
        [SerializeField] private float m_AfterburnPower;
        [SerializeField] private float m_AfterburnTimeLength;
        private bool m_IsInAfterburn;

        // Movement Fields
        [SerializeField] private float m_MoveRampTimeLength;
        private Vector2 m_MoveDirection;
        private float m_MoveInterpolation;

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

        #region - - - - - - Movement Methods - - - - - -

        void IPlayerMovement.SetLookDirection(Vector2 lookDirection) 
            => this.transform.rotation =
                Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90));

        void IPlayerMovement.SetMoveDirection(Vector2 moveDirection)
            => this.m_MoveDirection = moveDirection;
        
        private void RunMovement()
        {
            this.m_MoveInterpolation = Mathf.Clamp(
                this.m_MoveInterpolation + Time.deltaTime, 0,
                this.m_MoveRampTimeLength);
            
            if (this.m_MoveDirection == Vector2.zero && !this.m_IsInAfterburn)
            {
                this.m_Rigidbody.linearVelocity = Vector2.Lerp(
                    this.m_Rigidbody.linearVelocity, 
                    Vector2.zero,
                    this.m_MoveInterpolation);
                return;
            }
            
            this.m_Rigidbody.linearVelocity = Vector2.Lerp(
                this.m_Rigidbody.linearVelocity, 
                this.m_MoveDirection * this.m_TotalSpeed, 
                this.m_MoveInterpolation);
        }

        #endregion Movement Methods

        #region - - - - - - Afterburn Methods - - - - - -

        void IPlayerMovement.StartAfterburn()
        {
            if (this.m_IsInAfterburn) return;
            
            this.m_IsInAfterburn = true;
            this.StartCoroutine(this.RunAfterBurn());
        }

        void IPlayerMovement.EndAfterburn()
        {
            this.m_IsInAfterburn = false;
            this.StopAllCoroutines();
            this.StopAfterburn();
        }

        private IEnumerator RunAfterBurn()
        {
            float _SpeedBefore = this.m_TotalSpeed;
            float _TargetAfterburnSpeed = this.m_TotalSpeed + this.m_AfterburnPower;
            float _AfterburnRuntime = 0;
            float _AfterburnRampTime = 0;
            float _AfterburnRampTimeLength = this.m_AfterburnTimeLength * 0.1f;
            
            while (_AfterburnRuntime < this.m_AfterburnTimeLength)
            {
                this.m_TotalSpeed = Mathf.Lerp(
                    _SpeedBefore,
                    _TargetAfterburnSpeed,
                    _AfterburnRampTime / _AfterburnRampTimeLength);

                _AfterburnRuntime += Time.deltaTime;
                _AfterburnRampTime += Time.deltaTime;
                
                yield return null;
            }

            this.StopAfterburn();
        }

        private void StopAfterburn()
        {
            this.m_IsInAfterburn = false;
            this.m_TotalSpeed = this.m_ThrustPower;
        }

        #endregion Afterburn Methods
  
    }

}