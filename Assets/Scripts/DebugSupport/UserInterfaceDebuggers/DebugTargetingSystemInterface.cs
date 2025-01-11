using ProjectExodus.GameLogic.Player.PlayerTargetingSystem;
using TMPro;
using UnityEngine;

namespace ProjectExodus.DebugSupport.UserInterfaceDebuggers
{

    public class DebugTargetingSystemInterface : MonoBehaviour
    {
        public TMP_Text CurrentRaycastLabel;
        public TMP_Text TrackingBeamDistanceLengthLabel;

        
        public PlayerTargetingSystem PlayerTargetingSystem;
        public TractorBeamTrackingHandler TractorBeamTrackingHandler;
        
        public void Update()
        {
            if (this.PlayerTargetingSystem == null)
            {
                this.FindCurrentPlayer();
                return;
            }
            
            this.UpdateCurrentRaycastLabel();
            this.UpdateTrackingBeamDistanceLengthLabel();
        }

        private void UpdateCurrentRaycastLabel()
        {
            this.CurrentRaycastLabel.text = $"Current Raycast Target: {this.PlayerTargetingSystem.m_PossibleTarget}";
        }

        private void UpdateTrackingBeamDistanceLengthLabel()
        {
            this.TrackingBeamDistanceLengthLabel.text = "Tracking Beam Distance: " +
                (this.TractorBeamTrackingHandler.NextTargetTransform == null 
                    ? "0" 
                    : (this.TractorBeamTrackingHandler.NextTargetTransform.position - 
                        this.PlayerTargetingSystem.transform.position).sqrMagnitude);
        }

        private void FindCurrentPlayer()
        {
            this.PlayerTargetingSystem = FindFirstObjectByType<PlayerTargetingSystem>(FindObjectsInactive.Exclude);
            this.TractorBeamTrackingHandler = FindFirstObjectByType<TractorBeamTrackingHandler>(FindObjectsInactive.Exclude);
        }
    }

}