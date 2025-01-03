using System;
using System.Collections;
using ProjectExodus.UserInterface.TrackingSystemHUD.TractorBeamTrackingHUD;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerTargetingSystem
{

    // Actions of this behaviour
    // 1. Await for ctrl to be pressed to activate the general track
    // 2. Await for hover to exceed set time over an interactable object type
    // 3. Start the track
    // 4. Draw the beam line and the outline to the object (of blue shade)
    // 4.5 If within radius keep the beam line blue, but if distant make it red
    // 5. Move the target object's position to inverse-smooth-damp move to player's position
    // 6. Once object within a 'near radius', end the track.
    public class TractorBeamTrackingHandler
    {

        #region - - - - - - Fields - - - - - -
        
        private UnityEngine.Camera m_Camera;
        private Transform m_PlayerTransform;
        private int m_TargetObjectID;
        private Transform m_TargetTransform;
        private TractorBeamTrackingHUDController m_TractorBeamTrackingHUDController;

        private float m_MaxBeamLength = 8;
        private float m_TargetingLockOnTimeLength = 8f; // TODO: Change to use the virtual weight of the object.
        private bool m_CanTrack;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public TractorBeamTrackingHandler(Transform playerTransform)
        {
            this.m_PlayerTransform = playerTransform ?? throw new ArgumentNullException(nameof(playerTransform));
        }

        #endregion Constructors
  
        #region - - - - - - Properties - - - - - -

        public bool CanTrack => this.m_CanTrack;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void TrackTarget(out bool _HasStoppedTracking)
        {
            // Get position of object
            Vector2 _PlayerPosition = this.m_PlayerTransform.position;

            // Get position of player ship
            Vector2 _TargetPosition = this.m_TargetTransform.position;
            
            // Get sqr magnitude distance
            float _SqrMagnitude = (_TargetPosition - _PlayerPosition).sqrMagnitude;

            // Calculate beam stregth based on distance
            float _BeamStrength = Mathf.Clamp(this.m_MaxBeamLength - _SqrMagnitude, 0, 1);

            // If distance is too great, disengage beam.
            if (_BeamStrength <= 0)
            {
                _HasStoppedTracking = true;
                this.EndTargeting();
            }

            _HasStoppedTracking = false;
        }
        
        public void SetNewTarget(GameObject newTarget) 
            => this.m_TargetTransform = newTarget.transform;

        // Note: This time length should be changed to tractor based on the virtual weight of the object.
        public IEnumerator StartTargeting()
        {
            // yield return new WaitForSeconds(this.m_TargetingLockOnTimeLength);

            float _RemainingTime = this.m_TargetingLockOnTimeLength;
            bool _StopTracking = false;
            while (_RemainingTime > 0 && !_StopTracking)
            {
                _RemainingTime -= Time.deltaTime;
                
                this.TrackTarget(out bool _HasStoppedTracking);
                if (_HasStoppedTracking)
                    _StopTracking = true;

                yield return null;
            }
            
            Debug.Log("RunTractorLocking is started.");
            this.m_CanTrack = true;
        }

        public void EndTargeting()
        {
            this.m_TargetTransform = null;
            this.m_CanTrack = false;
        }
        
        #endregion Methods
  
    }

}