using System;
using UnityEngine;

namespace ProjectExodus
{

    public class WeaponTargetingHandler : MonoBehaviour, IInitialize<WeaponTargetingHandlerInitializationData>
    {

        #region - - - - - - Fields - - - - - -
        
        // Dependent components
        private Camera m_Camera;
        private IWeaponTrackingHUDController m_TargetWeaponTrackingHUDController;
        private Transform m_PlayerTransform;
        private Transform m_TargetTransform;

        private bool m_CanTrack;
        private float m_TargetSqrMagnitudeDistance;
        private float m_ScreenWidth;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public void Initialize(WeaponTargetingHandlerInitializationData initializationData)
        {
            this.m_Camera = initializationData.Camera ?? throw new ArgumentNullException(nameof(initializationData.Camera));
            this.m_TargetWeaponTrackingHUDController =
                initializationData.WeaponTrackingHUDController 
                    ?? throw new ArgumentNullException(nameof(initializationData.WeaponTrackingHUDController));

            this.m_PlayerTransform = this.transform;
            this.m_ScreenWidth = Screen.width;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void TrackTarget()
        {
            if (!this.m_CanTrack || this.m_TargetTransform == null) return;

            this.m_TargetSqrMagnitudeDistance =
                (new Vector2(this.m_TargetTransform.position.x, this.m_TargetTransform.position.y)
                 - new Vector2(this.m_PlayerTransform.position.x, this.m_PlayerTransform.position.y)).sqrMagnitude;

            // Check that the target has not been lost
            this.m_TargetWeaponTrackingHUDController.SetTargetCrosshairPosition(
                this.m_Camera.WorldToScreenPoint(this.m_TargetTransform.position));

            // TODO: This needs to change to the width of the screen.
            if (this.m_TargetSqrMagnitudeDistance > this.m_ScreenWidth)
                this.EndTargeting();
        }

        public void StartTargeting(GameObject newTarget)
        {
            // End targeting if already targeting the current target.
            if (this.m_TargetTransform != null
                && newTarget.GetInstanceID() == this.m_TargetTransform.gameObject.GetInstanceID())
            {
                this.EndTargeting();
                return;
            }

            this.m_TargetTransform = newTarget.transform;
            this.m_CanTrack = true;
            this.m_TargetWeaponTrackingHUDController.ShowScreen();
        }

        public void EndTargeting()
        {
            this.m_CanTrack = false;
            this.m_TargetTransform = null;
            this.m_TargetWeaponTrackingHUDController.HideScreen();
        }

        #endregion Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            if (!this.m_TargetTransform || !this.m_CanTrack) return;

            Vector3 _TransformPosition = this.m_TargetTransform.position;

            // Draw circular bounds
            Gizmos.color = new Color(255, 0, 0, 1);
            Gizmos.DrawWireSphere(_TransformPosition, this.m_TargetSqrMagnitudeDistance);

            // Draw max circular distance
            Gizmos.color = new Color(0, 255, 255, 1);
            Gizmos.DrawWireSphere(_TransformPosition, this.m_ScreenWidth);
        }

        #endregion

    }

    public class WeaponTargetingHandlerInitializationData
    {

        #region - - - - - - Properties - - - - - -

        public Camera Camera { get; set; }
        
        public IWeaponTrackingHUDController WeaponTrackingHUDController { get; set; }

        #endregion Properties
  
    }

}