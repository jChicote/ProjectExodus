using MBT;
using ProjectExodus.GameLogic.Common.Timers;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Repeat Can Run Check")]
    public class CanRunRepeaterCondition : Condition
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private BoolReference m_IsPaused = new();
        [SerializeField] private float m_IntervalTime;
        
        private EventTimer m_EventTimer;
        private bool m_CanRun;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start() 
            => this.m_EventTimer = new EventTimer(this.m_IntervalTime, Time.deltaTime, () => this.m_CanRun = true);

        private void Update()
        {
            if (this.m_IsPaused.Value) return;
            
            this.m_EventTimer.TickTimer();
        }
        
        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        public override bool Check()
        {
            if (!this.m_CanRun) return false;
            
            this.m_CanRun = false;
            return true;
        }

        #endregion Methods
  
    }

}