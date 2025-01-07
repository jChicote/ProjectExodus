using System;
using System.Collections;
using ProjectExodus.Utility.GameLogging;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using SceneManager = ProjectExodus.Management.SceneManager.SceneManager;

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
    public class TractorBeamTrackingHandler : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        private const float DISTANCE_PADDING = 20f;
        
        private UnityEngine.Camera m_Camera;
        private Transform m_PlayerTransform;
        private TractorBeamTrackingHUDController m_TractorBeamTrackingHUDController;

        private Transform m_CurrentTargetTransform;
        private Transform m_PossibleNextTargetTransform;

        private float m_MaxBeamLength = 100;
        private float m_TargetingLockOnTimeLength = 8f; // TODO: Change to use the virtual weight of the object.
        private bool m_CanTrack;

        private bool m_IsCurrentlyHoverTracking;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialise(Transform playerTransform, TractorBeamTrackingHUDController tractorBeamTrackingHUDController)
        {
            this.m_PlayerTransform = playerTransform ?? throw new ArgumentNullException(nameof(playerTransform));
            this.m_TractorBeamTrackingHUDController = tractorBeamTrackingHUDController ??
                                                      throw new ArgumentNullException(
                                                          nameof(tractorBeamTrackingHUDController));

            this.m_TractorBeamTrackingHUDController.PlayerTransform = playerTransform;

            this.m_Camera = SceneManager.Instance.GetActiveSceneController().Camera;
        }

        #endregion Initializers
  
        #region - - - - - - Properties - - - - - -

        public bool CanTrack => this.m_CanTrack;

        public Transform CurrentTargetTransform => this.m_CurrentTargetTransform;

        public Transform NextTargetTransform
        {
            get => this.m_PossibleNextTargetTransform;
            set => this.m_PossibleNextTargetTransform = value;
        }

        public bool IsHoverTargeting => this.m_IsCurrentlyHoverTracking;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void TrackCurrentTarget()
        {
            if (this.m_CurrentTargetTransform == null) return;
            
            if (this.IsLockedTargetOutsideTrackingBoundary()) 
                this.m_TractorBeamTrackingHUDController.EndTargetingLock();
        }

        public void LockAndTrackToTarget(out bool _HasStoppedTracking)
        {
            // if (this.m_PossibleNextTargetTransform == null)
            // {
            //     _HasStoppedTracking = true;
            //     return;
            // }
            
            Debug.Log("Is Locking");
            
            // Get position of object
            Vector2 _PlayerPosition = this.m_PlayerTransform.position;
            
            // Get position of player ship
            Vector2 _TargetPosition = this.m_PossibleNextTargetTransform.position;
            
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

        public bool IsLockedTargetOutsideTrackingBoundary()
        {
            if (this.m_CurrentTargetTransform == null) return false;
            
            float _SqrMagnitude =
                (this.m_PlayerTransform.position - this.m_CurrentTargetTransform.position).sqrMagnitude;
            
            // Calculate dimensions since the camera is orthographic
            float _VerticalHaldSqrHeight = this.m_Camera.orthographicSize * 2;
            float _VerticalHalfSqrWidth = this.m_Camera.aspect * _VerticalHaldSqrHeight;

            return _SqrMagnitude > 
                   new Vector2(_VerticalHalfSqrWidth, _VerticalHaldSqrHeight).sqrMagnitude + DISTANCE_PADDING;
        }

        private void ConfirmTrackedTarget()
        {
            this.m_CurrentTargetTransform = this.m_PossibleNextTargetTransform;
            this.m_TractorBeamTrackingHUDController.StartTargetingLock();
            this.m_TractorBeamTrackingHUDController.CurrentTargetTransform = this.m_PossibleNextTargetTransform;
            this.m_IsCurrentlyHoverTracking = false;
        }
        
        // public void SetNewTarget(GameObject newTarget)
        // {
        //     this.m_PossibleNextTargetTransform = newTarget.transform;
        //     GameLogger.Log(
        //         (nameof(m_PossibleNextTargetTransform),m_PossibleNextTargetTransform));
        //     this.m_TractorBeamTrackingHUDController.NextTargetTransform = this.m_PossibleNextTargetTransform;
        //     this.m_TractorBeamTrackingHUDController.StartTargetingSearch();
        // }

        public void StartHoverTargetLock(GameObject nextTarget)
        {
            // 2. Can cancel if attempting to retarget on a locked on object
            bool _IsRetargetingSameLockedOnObject = this.m_CurrentTargetTransform != null &&
                                                    this.m_CurrentTargetTransform.gameObject.GetInstanceID() ==
                                                    nextTarget.GetInstanceID();
            if (_IsRetargetingSameLockedOnObject)
            {
                this.EndLockedTargeting();
                return;
            }
            
            // 1. Can cancel if targeting an object but locked on another object without reseting the lock on object
            bool _IsRetargetingSameTrackingObject = this.m_PossibleNextTargetTransform != null &&
                                                    this.m_PossibleNextTargetTransform.gameObject.GetInstanceID() ==
                                                    nextTarget.GetInstanceID();
            if (_IsRetargetingSameTrackingObject)
            {
                this.EndTargeting();
                return;
            }
            
            this.m_PossibleNextTargetTransform = nextTarget.transform;
            this.m_TractorBeamTrackingHUDController.NextTargetTransform = nextTarget.transform;
            
            this.StopCoroutine(this.StartHoverTargeting()); // Stop any preexisting routine
            this.m_TractorBeamTrackingHUDController.StartTargetingSearch();

            Debug.Log("Has triggered Hover lock");
            
            this.StartCoroutine(StartHoverTargeting());
        }
        
        // Note: This time length should be changed to tractor based on the virtual weight of the object.
        public IEnumerator StartHoverTargeting()
        {
            // yield return new WaitForSeconds(this.m_TargetingLockOnTimeLength);
            this.m_IsCurrentlyHoverTracking = true;
            
            float _RemainingTime = this.m_TargetingLockOnTimeLength;
            bool _StopTracking = false;
            while (_RemainingTime > 0 && !_StopTracking)
            {
                _RemainingTime -= Time.deltaTime;
                
                // Update beam strength presentation
                float _CurrentDistance = (this.m_PlayerTransform.position - this.m_PossibleNextTargetTransform.position)
                    .sqrMagnitude;
                this.m_TractorBeamTrackingHUDController.SetBeamStrengthColor(_CurrentDistance, this.m_MaxBeamLength);
                
                this.LockAndTrackToTarget(out bool _HasStoppedTracking);
                if (_HasStoppedTracking)
                    _StopTracking = true;

                yield return null;
            }

            if (_RemainingTime < 0)
                this.ConfirmTrackedTarget();
            else
                this.EndTargeting();
            
            Debug.Log("RunTractorLocking is started.");
            this.m_CanTrack = true;
        }

        /// <summary>
        /// Ends all targeting.
        /// </summary>
        private void EndTargeting()
        {
            this.StopAllCoroutines();
            this.m_TractorBeamTrackingHUDController.EndTargetingSearch();
            
            this.m_PossibleNextTargetTransform = null;
            this.m_IsCurrentlyHoverTracking = false;
            this.m_CanTrack = false;
        }

        private void EndLockedTargeting()
        {
            this.StopAllCoroutines();
            this.m_TractorBeamTrackingHUDController.EndTargetingLock();
            this.m_CurrentTargetTransform = null;
        }
        
        #endregion Methods

        #region - - - - - - Debug GUI - - - - - -

        private void OnGui()
        {
            GUILayout.Label($"Player Position: {transform.position}");
        }

        #endregion Debug GUI
  
    }

}