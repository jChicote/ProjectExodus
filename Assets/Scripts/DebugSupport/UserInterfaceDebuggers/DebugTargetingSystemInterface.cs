using ProjectExodus.GameLogic.Player.PlayerTargetingSystem;
using TMPro;
using UnityEngine;

namespace ProjectExodus.DebugSupport.UserInterfaceDebuggers
{

    public class DebugTargetingSystemInterface : MonoBehaviour
    {
        public TMP_Text CurrentRaycastLabel;

        public PlayerTargetingSystem PlayerTargetingSystem;
        
        public void Update()
        {
            if (this.PlayerTargetingSystem == null)
            {
                this.FindCurrentPlayer();
                return;
            }
            
            this.UpdateCurrentRaycastLabel();
        }

        private void UpdateCurrentRaycastLabel()
        {
            this.CurrentRaycastLabel.text = $"Current Raycast Target: {this.PlayerTargetingSystem.m_PossibleTarget}";
        }

        private void FindCurrentPlayer()
        {
            this.PlayerTargetingSystem = FindFirstObjectByType<PlayerTargetingSystem>(FindObjectsInactive.Exclude);
        }
    }

}