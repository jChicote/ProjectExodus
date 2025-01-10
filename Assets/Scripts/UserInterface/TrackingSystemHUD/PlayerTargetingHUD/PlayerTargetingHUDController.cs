using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.ScriptableObjects;
using UnityEngine;

namespace ProjectExodus
{

    public class PlayerTargetingHUDController : 
        MonoBehaviour, 
        IPlayerTargetingHUDController
    {

        #region - - - - - - Fields - - - - - -

        private PlayerTargetingHUDView m_View;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize(PlayerTargetingHUDData initializationData)
        {
            this.m_View = this.GetComponent<PlayerTargetingHUDView>();
            this.m_View.Initialize(initializationData);
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        public void UpdateHoverTargetingIndicatorPosition(Vector2 screenPosition)
            => this.m_View.UpdateHoverTargetingRecticlePosition(screenPosition);

        public void UpdateIndicatorPresentation(string objectTag)
            => this.m_View.UpdateHoverTargetingRecticleColor(GameTag.IsValid(objectTag)
                ? objectTag
                : GameTag.Default);

        public void ShowHoverTargetingIndicator()
            => this.m_View.ShowHoverTargetingRecticle();

        public void HideHoverTargetingIndicator()
            => this.m_View.HideHoverTargetingRecticle();

        #endregion Methods

    }
    
    public class PlayerTargetingHUDData
    {

        #region - - - - - - Properties - - - - - -

        public Camera Camera { get; set; }
        
        public UserInterfaceSettings UserInterfaceSettings { get; set; }

        #endregion Properties
  
    }

}
