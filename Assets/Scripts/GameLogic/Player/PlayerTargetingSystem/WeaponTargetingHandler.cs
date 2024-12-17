using System;
using ProjectExodus.UserInterface.TrackingSystemHUD.TargetTrackingHUD;
using ProjectExodus.Utility.GameLogging;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerTargetingSystem
{

    public class WeaponTargetingHandler
    {
        
        #region - - - - - - Fields - - - - - -
        
        private UnityEngine.Camera m_Camera;
        private ITrackingHUDController m_TargetTrackingHUDController;
        private Transform m_TargetTransform;
        
        private bool m_CanTrack;
        private float m_PointerRange;
        private Vector2 m_PointerPosition = Vector2.zero;
        private float m_TargetSqrMagnitudeDistance;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public WeaponTargetingHandler(
            float targetToPointerRange,
            UnityEngine.Camera camera,
            ITrackingHUDController trackingHUDController)
        {
            this.m_PointerRange = targetToPointerRange;
            this.m_Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            this.m_TargetTrackingHUDController =
                trackingHUDController ?? throw new ArgumentNullException(nameof(trackingHUDController));
        }

        #endregion Constructors
  
        #region - - - - - - Properties - - - - - -

        public bool CanTrack => this.m_CanTrack;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void TrackTarget()
        {
            if (!this.m_CanTrack || this.m_TargetTransform == null) return;
            
            this.m_TargetSqrMagnitudeDistance = 
                (new Vector2(
                    x: this.m_TargetTransform.position.x, 
                    y: this.m_TargetTransform.position.y) 
                 - this.m_PointerPosition).sqrMagnitude;

            // Check that the target has not been lost
            this.m_TargetTrackingHUDController.SetTargetCrosshairPosition(
                this.m_Camera.WorldToScreenPoint(this.m_TargetTransform.position));
            
            // TODO: This needs to change to the width of the screen.
            if (this.m_TargetSqrMagnitudeDistance > this.m_PointerRange * this.m_PointerRange) 
                this.EndWeaponTargeting();
        }
        
        public void SetNewTarget(GameObject newTarget) 
            => this.m_TargetTransform = newTarget.transform;

        public void SetPointerPosition(Vector2 position)
            => this.m_PointerPosition = position;

        public void StartWeaponTargeting()
        {
            GameLogger.Log("RunTractorLocking is started.");
            
            this.m_CanTrack = true;
            this.m_TargetTrackingHUDController.ShowScreen();
        }

        public void EndWeaponTargeting()
        {
            GameLogger.Log("RunTractorLocking has ended.");
            
            this.m_CanTrack = false;
            this.m_TargetTransform = null;
            this.m_TargetTrackingHUDController.HideScreen();
        }

        #endregion Methods

        #region - - - - - - Gizmos - - - - - -

        public void CalculateDrawGizmos(out Vector2 _TransformPosition, out float _Magnitude)
        {
            if (!this.m_TargetTransform || !this.m_CanTrack)
            {
                _TransformPosition = Vector2.zero;
                _Magnitude = 0;
                return;
            };
            
            float _SqrMagnitude = 
                ((new Vector2(
                    x: this.m_TargetTransform.position.x, 
                    y: this.m_TargetTransform.position.y) - this.m_PointerPosition))
                .magnitude;

            _TransformPosition = this.m_TargetTransform.position;
            _Magnitude = _SqrMagnitude;

        }

        #endregion 
  
    }

}