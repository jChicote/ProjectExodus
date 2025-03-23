using UnityEngine;

namespace ProjectExodus
{

    public interface ITractorBeamTrackingHUDController : IInitialize<TractorBeamTrackingHUDData>
    {

        #region - - - - - - Properties - - - - - -

        Transform PlayerTransform { get; set; }

        Transform NextTargetTransform { get; set; }

        Transform CurrentTargetTransform { get; set; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -
        
        // -------------------------------------
        // Searching and tracking target
        // -------------------------------------
        
        void StartTargetingSearch();
        
        void EndTargetingSearch();
        
        void SetBeamStrengthColor(float currentDistance, float maxDistance);
        
        void UpdateTimerCircle(float currentTime, float maxTime);
        
        // -------------------------------------
        // Locking and following the target
        // -------------------------------------
        
        void StartTargetingLock();
        
        void EndTargetingLock();
        
        // -------------------------------------
        // Presenting the OutOfRange warning
        // -------------------------------------
        
        void SetOutOfRangeCircleSize(float distance);
        
        void ShowOutOfRange();
        
        void HideScreen();

        void ShowScreen();

        #endregion Methods

    }

}