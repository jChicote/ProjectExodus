using System;
using System.Collections;
using ProjectExodus;
using UnityEngine;
using SceneManager = ProjectExodus.Management.SceneManager.SceneManager;

public class TractorBeamTrackingHandler : MonoBehaviour, IDebuggingDataProvider<TractorBeamTrackingHandlerData>
{

    #region - - - - - - Fields - - - - - -

    private const float DISTANCE_PADDING = 20f;

    private readonly float m_MaxBeamLength = 100;
    private readonly float m_TargetingLockOnTimeLength = 8f; // TODO: Change to use the virtual weight of the object.
    
    // Dependent components
    private Camera m_Camera;
    private TractorBeamTrackingHUDController m_TractorBeamTrackingHUDController;
    
    // Required target calculation transforms
    private Transform m_PlayerTransform;
    private Transform m_CurrentTargetTransform;
    private Transform m_PossibleNextTargetTransform;

    #endregion Fields

    #region - - - - - - Initializers - - - - - -

    public void Initialise(Transform playerTransform, TractorBeamTrackingHUDController tractorBeamTrackingHUDController)
    {
        this.m_PlayerTransform = playerTransform 
            ?? throw new ArgumentNullException(nameof(playerTransform));
        this.m_TractorBeamTrackingHUDController = tractorBeamTrackingHUDController 
            ?? throw new ArgumentNullException(nameof(tractorBeamTrackingHUDController));

        this.m_TractorBeamTrackingHUDController.PlayerTransform = playerTransform;
        this.m_Camera = SceneManager.Instance.GetActiveSceneController().Camera;
    }

    #endregion Initializers

    #region - - - - - - Properties - - - - - -

    public Transform NextTargetTransform 
        => this.m_PossibleNextTargetTransform;

    #endregion Properties

    #region - - - - - - Methods - - - - - -

    public void TrackCurrentTarget()
    {
        if (this.m_CurrentTargetTransform == null) return;
        
        if (this.IsLockedTargetOutsideTrackingBoundary()) 
            this.m_TractorBeamTrackingHUDController.EndTargetingLock();
    }

    public void StartHoverTargetLock(GameObject nextTarget)
    {
        // End targeting if attempting to retarget an already locked on target.
        if (this.m_CurrentTargetTransform != null &&
            this.m_CurrentTargetTransform.gameObject.GetInstanceID() ==
            nextTarget.GetInstanceID())
        {
            this.EndLockedTargeting();
            return;
        }
        
        // End targeting if already targeting the possible target.
        if (this.m_PossibleNextTargetTransform != null &&
            this.m_PossibleNextTargetTransform.gameObject.GetInstanceID() ==
            nextTarget.GetInstanceID())
        {
            this.EndTargeting();
            return;
        }
        
        this.m_PossibleNextTargetTransform = nextTarget.transform;
        this.m_TractorBeamTrackingHUDController.NextTargetTransform = nextTarget.transform;
        
        this.StopCoroutine(this.StartHoverTargeting());
        this.StartCoroutine(StartHoverTargeting());
        this.m_TractorBeamTrackingHUDController.StartTargetingSearch();
    }

    TractorBeamTrackingHandlerData IDebuggingDataProvider<TractorBeamTrackingHandlerData>.GetData()
        => new()
        {
            CurrentLockedOnTransform = this.m_CurrentTargetTransform,
            NextTrackingTransform = this.m_PossibleNextTargetTransform,
            PlayerTransform = this.m_PlayerTransform
        };
    
    private IEnumerator StartHoverTargeting()
    {
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
    }
    
    private void LockAndTrackToTarget(out bool _HasStoppedTracking)
    {
        float _SqrMagnitude = (this.m_PossibleNextTargetTransform.position - this.m_PlayerTransform.position)
            .sqrMagnitude;
        float _BeamStrength = Mathf.Clamp(this.m_MaxBeamLength - _SqrMagnitude, 0, 1);
        
        // If distance is too great, disengage beam.
        if (_BeamStrength <= 0)
        {
            _HasStoppedTracking = true;
            this.EndTargeting();
        }
    
        _HasStoppedTracking = false;
    }

    private bool IsLockedTargetOutsideTrackingBoundary()
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
    }
    
    private void EndTargeting()
    {
        this.StopAllCoroutines();
        this.m_TractorBeamTrackingHUDController.EndTargetingSearch();
        this.m_PossibleNextTargetTransform = null;
    }

    private void EndLockedTargeting()
    {
        this.StopAllCoroutines();
        this.m_TractorBeamTrackingHUDController.EndTargetingLock();
        this.m_CurrentTargetTransform = null;
    }
    
    #endregion Methods

}

public class TractorBeamTrackingHandlerData
{

    #region - - - - - - Properties - - - - - -

    public Transform CurrentLockedOnTransform { get; set; }
    
    public Transform NextTrackingTransform { get; set; }
    
    public Transform PlayerTransform { get; set; }

    #endregion Properties

}

