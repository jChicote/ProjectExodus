using UnityEngine;

namespace ProjectExodus
{

    public interface IPlayerTargetingHUDController : IInitialize<PlayerTargetingHUDData>
    {

        #region - - - - - - Methods - - - - - -

        void UpdateHoverTargetingIndicatorPosition(Vector2 screenPosition);

        void UpdateIndicatorPresentation(string objectTag);

        void ShowHoverTargetingIndicator();

        void HideHoverTargetingIndicator();

        #endregion Methods

    }


}