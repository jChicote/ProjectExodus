using System;
using ProjectExodus;
using UnityEngine;

public class WeaponTargetingHandler : MonoBehaviour
{
    
    #region - - - - - - Fields - - - - - -
    
    // Dependent components
    private Camera m_Camera;
    private IWeaponTrackingHUDController m_TargetWeaponTrackingHUDController;
    private Transform m_TargetTransform;
    
    private bool m_CanTrack;
    private float m_PointerRange;
    private Vector2 m_PointerPosition = Vector2.zero;
    private float m_TargetSqrMagnitudeDistance;

    #endregion Fields

    #region - - - - - - Constructors - - - - - -

    public void Initialize(
        float targetToPointerRange,
        Camera camera,
        IWeaponTrackingHUDController weaponTrackingHUDController)
    {
        this.m_PointerRange = targetToPointerRange;
        this.m_Camera = camera ?? throw new ArgumentNullException(nameof(camera));
        this.m_TargetWeaponTrackingHUDController =
            weaponTrackingHUDController ?? throw new ArgumentNullException(nameof(weaponTrackingHUDController));
    }

    #endregion Constructors

    #region - - - - - - Properties - - - - - -

    public bool CanTrack => this.m_CanTrack; // Only used for debug gizmos on PlayerTargetingSystem.

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
        this.m_TargetWeaponTrackingHUDController.SetTargetCrosshairPosition(
            this.m_Camera.WorldToScreenPoint(this.m_TargetTransform.position));
        
        // TODO: This needs to change to the width of the screen.
        if (this.m_TargetSqrMagnitudeDistance > this.m_PointerRange * this.m_PointerRange) 
            this.EndTargeting();
    }
    
    public void SetPointerPosition(Vector2 position)
        => this.m_PointerPosition = position;

    public void StartTargeting(GameObject newTarget)
    {
        // End targeting if already targeting the current target.
        if (this.m_TargetTransform != null
            && newTarget.GetInstanceID() ==this.m_TargetTransform.gameObject.GetInstanceID())
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

    public void CalculateDrawGizmos(out Vector2 _TransformPosition, out float _Magnitude)
    {
        if (!this.m_TargetTransform || !this.m_CanTrack)
        {
            _TransformPosition = Vector2.zero;
            _Magnitude = 0;
            return;
        };
        
        float _SqrMagnitude = 
            (new Vector2(
                x: this.m_TargetTransform.position.x, 
                y: this.m_TargetTransform.position.y) - this.m_PointerPosition)
            .magnitude;

        _TransformPosition = this.m_TargetTransform.position;
        _Magnitude = _SqrMagnitude;
    }

    #endregion 

}
