using MBT;
using ProjectExodus.GameLogic.Common.Timers;
using UnityEngine;

namespace ProjectExodus
{

    public class RunWithCooldownCondition : Condition
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private BoolReference m_IsPaused = new();
        [SerializeField] private float m_CooldownTime;
        [SerializeField] private float m_ActiveTime;
        
        private EventTimer m_CooldownTimer;
        private EventTimer m_ActiveTimer;

        private bool m_IsCooldown;
        private bool m_IsActive;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_CooldownTimer = new EventTimer(this.m_CooldownTime, Time.deltaTime, this.ActivateRunCheck);
            this.m_ActiveTimer = new EventTimer(this.m_ActiveTime, Time.deltaTime, this.ActivateCooldown);

            this.m_IsActive = true;
        }

        private void Update()
        {
            if (this.m_IsPaused.Value) return;
            
            if (this.m_IsActive)
                this.m_CooldownTimer.TickTimer();
            else if (this.m_IsCooldown)
                this.m_ActiveTimer.TickTimer();
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        public override bool Check() 
            => this.m_IsActive;

        private void ActivateRunCheck()
        {
            this.m_IsActive = true;
            this.m_IsCooldown = false;
        }

        private void ActivateCooldown()
        {
            this.m_IsActive = false;
            this.m_IsCooldown = true;
        }

        #endregion Methods
  
    }

}