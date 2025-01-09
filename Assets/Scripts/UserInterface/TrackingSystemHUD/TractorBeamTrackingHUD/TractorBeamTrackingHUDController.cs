using System.Collections;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.UserInterface;
using UnityEngine;

namespace ProjectExodus
{

    public class TractorBeamTrackingHUDController : PausableMonoBehavior, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        public Camera m_Camera; // Inject
        public TractorBeamTrackingHUDView m_View;

        #endregion Fields
  
        #region - - - - - - Properties - - - - - -

        public Transform PlayerTransform { get; set; }

        public Transform NextTargetTransform { get; set; }
        
        public Transform CurrentTargetTransform { get; set; }

        #endregion Properties

        #region - - - - - - Unity Methods - - - - - -

        private void LateUpdate()
        {
            if (this.m_IsPaused || this.PlayerTransform == null) return;
            
            this.SetNextTargetToFollow();
            this.SetConfirmedTarget();
        }

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -

        public void SetNextTargetToFollow()
        {
            if (this.NextTargetTransform == null) return;
            
            this.m_View.SetLinePositions(this.PlayerTransform.position, this.NextTargetTransform.position);
            this.m_View.UpdateCircle(1, this.m_Camera.WorldToScreenPoint(this.NextTargetTransform.position));
        }

        public void SetConfirmedTarget()
        {
            if (this.CurrentTargetTransform == null) return;
            
            this.m_View.UpdateRecticle(this.m_Camera.WorldToScreenPoint(this.CurrentTargetTransform.position));
        }

        public void SetBeamStrengthColor(float currentDistance, float maxDistance)
        {
            float _Strength = currentDistance / maxDistance;
            this.m_View.UpdateBeamStrengthColor(_Strength);
        }

        public void ShowOutOfRange()
        {
            this.StopCoroutine(this.StartRevealingOutOfRange());
            this.m_View.ShowOutOfRangeElements();
        }

        public void HideOutOfRange() 
            => this.m_View.HideOutofRangeElements();

        public void StartTargetingSearch()
        {
            this.m_View.ShowCircle();
            this.m_View.ShowLineBeam();
        }

        public void StartTargetingLock()
        {
            this.m_View.ShowRecticle();
            this.m_View.HideCircle();
            this.m_View.HideLineBeam();
        }

        public void EndTargetingSearch()
        {
            this.m_View.HideCircle();
            this.m_View.HideLineBeam();
        }

        public void EndTargetingLock() 
            => this.m_View.HideRecticle();

        public void HideScreen() 
            => this.m_View.HideHUD();

        public void ShowScreen() 
            => this.m_View.ShowHUD();

        private IEnumerator StartRevealingOutOfRange()
        {
            yield return new WaitForSeconds(2.5f);
            Debug.Log("Is Done");
            this.HideOutOfRange();
        }

        #endregion Methods
  
    }

}
