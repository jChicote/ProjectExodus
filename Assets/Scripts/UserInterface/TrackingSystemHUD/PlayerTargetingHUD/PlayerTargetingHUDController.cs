using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;

namespace ProjectExodus
{

    public class PlayerTargetingHUDController : MonoBehaviour, IPlayerTargetingHUDController
    {

        #region - - - - - - Fields - - - - - -

        private PlayerTargetingHUDView m_View;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void Initialize()
            => this.m_View = this.GetComponent<PlayerTargetingHUDView>();

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

}
