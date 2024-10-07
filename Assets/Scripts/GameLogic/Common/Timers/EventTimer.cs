using System;

namespace ProjectExodus.GameLogic.Common.Timers
{

    public class EventTimer
    {

        #region - - - - - - Fields - - - - - -

        private Action m_StartingAction;
        private Action m_CompletionAction;
        private float m_DeltaTime;
        private float m_TimeLeft;
        private float m_TimerLength;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public EventTimer(float timerLength, float deltaTime, Action action)
        {
            this.m_CompletionAction = action;
            this.m_DeltaTime = deltaTime;
            this.m_TimeLeft = timerLength;
            this.m_TimerLength = timerLength;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void TickTimer()
        {
            this.m_TimeLeft -= this.m_DeltaTime;

            if (!this.IsTimerComplete()) return;
            this.m_CompletionAction?.Invoke();
            this.ResetTimer();
        }

        public void ResetTimer() 
            => this.m_TimeLeft = this.m_TimerLength;

        private bool IsTimerComplete()
            => this.m_TimeLeft <= 0;
        
        #endregion Methods

    }

}