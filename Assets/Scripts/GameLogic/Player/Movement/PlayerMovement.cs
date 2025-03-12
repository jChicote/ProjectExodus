using System.Collections;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectExodus.GameLogic.Player.Movement
{

    public class PlayerMovement : PausableMonoBehavior, IPlayerMovement
    {

        #region - - - - - - Fields - - - - - -

        // Required Components Fields
        [SerializeField] private Rigidbody2D m_Rigidbody;
        private IGameplayHUDController m_GameplayHUDController;

        // Thrust Fields
        [SerializeField] private float m_MaxThrustMagnitude;
        [SerializeField] private float m_ThrustPower;
        private float m_TotalSpeed;
        
        // Afterburn Fields
        [SerializeField] private float m_AfterburnPower;
        [SerializeField] private float m_AfterburnFillAmount;
        [SerializeField] private float m_AfterburnCooldownTimeLength;
        private float m_CurrentAfterburnFill;
        private bool m_IsInAfterburn;
        private bool m_HasDepletedAfterburn;

        // Movement Fields
        [SerializeField] private float m_MoveRampTimeLength;
        private Vector2 m_MoveDirection;
        private float m_MoveInterpolation;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            UserInterfaceManager.Instance.GetTheActiveUserInterfaceController().TryGetGUIControllers(out object _Controllers);
            this.m_GameplayHUDController = ((GameplaySceneGUIControllers)_Controllers).GetGameplayHUDController();
            this.m_GameplayHUDController.SetAfterburnFill(1, 1);
            
            this.m_TotalSpeed = this.m_ThrustPower;
        }

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
            if (this.m_IsInAfterburn || this.m_HasDepletedAfterburn) return;
            
            this.m_IsInAfterburn = true;
            this.StopAllCoroutines();
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
            float _AfterburnFillTime = 0;
            float _AfterburnFillTimeLength = this.m_AfterburnFillAmount * 0.1f;
            
            while (this.m_CurrentAfterburnFill < this.m_AfterburnFillAmount)
            {
                this.m_TotalSpeed = Mathf.Lerp(
                    _SpeedBefore,
                    _TargetAfterburnSpeed,
                    _AfterburnFillTime / _AfterburnFillTimeLength);
                this.m_GameplayHUDController.SetAfterburnFill(
                    this.m_AfterburnFillAmount - this.m_CurrentAfterburnFill, 
                    this.m_AfterburnFillAmount);

                this.m_CurrentAfterburnFill += Time.deltaTime;
                _AfterburnFillTime += Time.deltaTime;
                
                yield return null;
            }

            this.m_HasDepletedAfterburn = true;
            this.StopAfterburn();
        }

        private void StopAfterburn()
        {
            this.m_IsInAfterburn = false;
            this.m_TotalSpeed = this.m_ThrustPower;
            this.StartCoroutine(this.RunAfterburnCooldown());
        }

        private IEnumerator RunAfterburnCooldown()
        {
            float _CooldownRuntime = 0;
            
            // Briefly pause cooldown to delay the perceived 'quickness' of the cooldown.
            float _CooldownPauseTime = this.m_AfterburnCooldownTimeLength * 0.2f;
            yield return new WaitForSeconds(_CooldownPauseTime);

            while (_CooldownRuntime < this.m_AfterburnCooldownTimeLength && this.m_CurrentAfterburnFill > 0)
            {
                float _CooldownStepInterval = this.m_AfterburnFillAmount / this.m_AfterburnCooldownTimeLength * Time.deltaTime;
                this.m_CurrentAfterburnFill = Mathf.Clamp(
                    this.m_CurrentAfterburnFill - _CooldownStepInterval, 
                    0, 
                    this.m_AfterburnFillAmount);
                this.m_GameplayHUDController.SetAfterburnFill(
                    this.m_AfterburnFillAmount - this.m_CurrentAfterburnFill, 
                    this.m_AfterburnFillAmount);

                _CooldownRuntime += Time.deltaTime;
                yield return null;
            }

            this.m_HasDepletedAfterburn = false;
        } 

        #endregion Afterburn Methods
  
    }

}