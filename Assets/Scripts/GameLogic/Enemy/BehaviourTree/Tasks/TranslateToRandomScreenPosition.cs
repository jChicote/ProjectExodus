using MBT;
using ProjectExodus.GameLogic.Common.Timers;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Translate To Random Screen Position")]
    public class TranslateToRandomScreenPosition : Leaf
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] 
        private TransformReference m_SourceTransform = new();
        [SerializeField] 
        private float m_TimeBetweenTravel;
        [SerializeField] 
        private float m_DurationOfTravel;

        private EventTimer m_EventTimer;
        private bool m_CanTravel;
        private Vector2 m_NextTravelPosition;
        private Vector2 m_StartingTravelPosition;
        private float m_ElapsedTime;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_EventTimer = new EventTimer(this.m_TimeBetweenTravel, Time.deltaTime, this.StartTravel);
        }

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -

        public override NodeResult Execute()
        {
            if (!this.m_CanTravel)
            {
                this.m_EventTimer.TickTimer();
                return NodeResult.success;
            }

            if (this.m_ElapsedTime > this.m_DurationOfTravel)
            {
                this.m_CanTravel = false;
                this.ResetTranslationValues();
                return NodeResult.success;
            }

            this.m_ElapsedTime += Time.deltaTime;
            Vector2 _NewPosition = Vector2.Lerp(
                this.m_StartingTravelPosition,
                this.m_NextTravelPosition,
                Mathf.Clamp(0, 1, this.m_ElapsedTime / this.m_DurationOfTravel));

            this.m_SourceTransform.Value.position = new(_NewPosition.x, _NewPosition.y, 0);
            return NodeResult.success;
        }

        private void StartTravel()
        {
            this.m_CanTravel = true;
            Vector3 _RandomPosition = new(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
            Vector3 _WorldPoint = Camera.main.ViewportToWorldPoint(_RandomPosition);
            this.m_NextTravelPosition = new(_WorldPoint.x, _WorldPoint.y);
            this.m_StartingTravelPosition = this.m_SourceTransform.Value.position;
        }

        private void ResetTranslationValues()
        {
            this.m_StartingTravelPosition = Vector2.zero;
            this.m_NextTravelPosition = Vector2.zero;
            this.m_ElapsedTime = 0;
        }

        #endregion Methods
        
    }

}