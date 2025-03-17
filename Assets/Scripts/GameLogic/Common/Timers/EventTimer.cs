using System;

namespace ProjectExodus.GameLogic.Common.Timers
{

    public class EventTimer
    {

        #region - - - - - - Fields - - - - - -

        private Action m_EndingAction;
        private float m_DeltaTime;
        private float m_TimeLeft;
        private float m_TimerLength;
        private bool m_CanRun;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public EventTimer(float timerLength, float deltaTime, Action endingAction, bool canRun = true)
        {
            this.m_CanRun = canRun;
            this.m_EndingAction = endingAction;
            this.m_DeltaTime = deltaTime;
            this.m_TimeLeft = timerLength;
            this.m_TimerLength = timerLength;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public float TimerLength
        {
            get => this.m_TimerLength;
            set => this.m_TimerLength = value;
        }
        
        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void TickTimer()
        {
            if (!this.m_CanRun) return;
            
            this.m_TimeLeft -= this.m_DeltaTime;

            if (!this.IsTimerComplete()) return;
            this.m_EndingAction?.Invoke();
            this.ResetTimer();
        }

        public void ResetTimer() 
            => this.m_TimeLeft = this.m_TimerLength;

        public void EnableTimer()
            => this.m_CanRun = true;
        
        public void DisableTimer()
            => this.m_CanRun = false;
        
        private bool IsTimerComplete()
            => this.m_TimeLeft <= 0;
        
        #endregion Methods

    }

}