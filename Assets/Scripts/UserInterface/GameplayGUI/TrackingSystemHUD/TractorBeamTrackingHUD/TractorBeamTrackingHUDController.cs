using System;
using System.Collections;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.ScriptableObjects;
using UnityEngine;

namespace ProjectExodus
{

    public class TractorBeamTrackingHUDController : 
        PausableMonoBehavior,
        ITractorBeamTrackingHUDController
    {

        #region - - - - - - Fields - - - - - -

        private Camera m_Camera;
        private TractorBeamTrackingHUDView m_View;

        private float m_TimerSize = 5f;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Transform PlayerTransform { get; set; }

        public Transform NextTargetTransform { get; set; }

        public Transform CurrentTargetTransform { get; set; }

        #endregion Properties

        #region - - - - - - Initializers - - - - - -

        public void Initialize(TractorBeamTrackingHUDData initializerData)
        {
            this.m_Camera = initializerData.Camera ?? throw new ArgumentNullException(nameof(initializerData.Camera));
            this.m_View = this.GetComponent<TractorBeamTrackingHUDView>();
            this.m_View.Initialize(initializerData);
        }

        #endregion Initializers
  
        #region - - - - - - Unity Methods - - - - - -

        private void LateUpdate()
        {
            if (this.m_IsPaused || this.PlayerTransform == null) return;

            this.SetNextTargetToFollow();
            this.SetConfirmedTarget();
        }

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -
        
        // -------------------------------------
        // Searching and tracking target
        // -------------------------------------

        public void StartTargetingSearch()
        {
            this.m_View.ShowCircle();
            this.m_View.ShowLineBeam();
        }

        public void EndTargetingSearch()
        {
            this.m_View.HideCircle();
            this.m_View.HideLineBeam();
        }

        public void SetBeamStrengthColor(float currentDistance, float maxDistance)
        {
            float _Strength = currentDistance / maxDistance;
            this.m_View.UpdateBeamStrengthColor(_Strength);
        }

        public void UpdateTimerCircle(float currentTime, float maxTime)
            => this.m_View.UpdateCircleSize((currentTime / maxTime) * this.m_TimerSize);
        
        private void SetNextTargetToFollow()
        {
            if (this.NextTargetTransform == null) return;

            this.m_View.SetLinePositions(this.PlayerTransform.position, this.NextTargetTransform.position);
            this.m_View.UpdateCirclePosition(this.m_Camera.WorldToScreenPoint(this.NextTargetTransform.position));
        }
        
        // -------------------------------------
        // Locking and following the target
        // -------------------------------------

        public void StartTargetingLock()
        {
            this.m_View.ShowRecticle();
            this.m_View.HideCircle();
            this.m_View.HideLineBeam();
        }

        public void EndTargetingLock()
            => this.m_View.HideRecticle();

        private void SetConfirmedTarget()
        {
            if (this.CurrentTargetTransform == null) return;
            this.m_View.UpdateRecticle(this.m_Camera.WorldToScreenPoint(this.CurrentTargetTransform.position));
        }
        
        // -------------------------------------
        // Presenting the OutOfRange warning
        // -------------------------------------

        // The distance is passed as SqrMagnitude. Alter this so that its approximate to the magnitude distance.
        public void SetOutOfRangeCircleSize(float distance)
        {
            // This may be expensive
            this.m_View.UpdateOutOfRangeCircleSize(Mathf.Sqrt(distance));
            this.m_View.UpdateOutOfRangeCirclePosition(this.m_Camera.WorldToScreenPoint(this.PlayerTransform.position));
        }

        public void ShowOutOfRange()
        {
            this.StopCoroutine(this.StartRevealingOutOfRange());
            this.StartCoroutine(this.StartRevealingOutOfRange());
            this.m_View.ShowOutOfRangeElements();
        }

        private void HideOutOfRange()
            => this.m_View.HideOutofRangeElements();

        private IEnumerator StartRevealingOutOfRange()
        {
            yield return new WaitForSeconds(2.5f);
            this.HideOutOfRange();
        }

        public void HideScreen()
            => this.m_View.HideHUD();

        public void ShowScreen()
            => this.m_View.ShowHUD();

        #endregion Methods

    }

    public class TractorBeamTrackingHUDData
    {

        #region - - - - - - Properties - - - - - -

        public Camera Camera { get; set; }
        
        public UserInterfaceSettings UserInterfaceSettings { get; set; }

        #endregion Properties
  
    }

}
