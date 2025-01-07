using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using ProjectExodus.UserInterface;
using ProjectExodus.Utility.GameLogging;
using UnityEngine;

namespace ProjectExodus
{

    public class TractorBeamTrackingHUDController : PausableMonoBehavior, IScreenStateController
    {
        public Camera m_Camera; // Inject
        public TractorBeamTrackingHUDView m_View;
        
        public Transform PlayerTransform { get; set; }

        public Transform NextTargetTransform { get; set; }
        
        public Transform CurrentTargetTransform { get; set; }
        
        
        private void LateUpdate()
        {
            if (this.m_IsPaused || this.PlayerTransform == null) return;
            
            this.SetNextTargetToFollow();
            this.SetConfirmedTarget();
        }

        public void SetNextTargetToFollow()
        {
            if (this.NextTargetTransform == null) return;
            
            GameLogger.Log(
                (nameof(PlayerTransform), PlayerTransform),
                (nameof(NextTargetTransform), NextTargetTransform),
                (nameof(m_Camera),m_Camera));
            
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

        public void StartTargetingSearch()
        {
            Debug.Log("Targeting has started");
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
        {
            this.m_View.HideRecticle();
        }


        public void HideScreen()
        {
            this.m_View.HideHUD();
        }

        public void ShowScreen()
        {
            this.m_View.ShowHUD();
        }
    }

}