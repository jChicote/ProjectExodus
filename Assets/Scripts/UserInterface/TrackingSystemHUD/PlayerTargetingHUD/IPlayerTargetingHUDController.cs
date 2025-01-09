using UnityEngine;

namespace ProjectExodus
{

    public interface IPlayerTargetingHUDController
    {

        #region - - - - - - Methods - - - - - -

        void Initialize();

        void UpdateHoverTargetingIndicatorPosition(Vector2 screenPosition);

        void UpdateIndicatorPresentation(string objectTag);

        void ShowHoverTargetingIndicator();

        void HideHoverTargetingIndicator();

        #endregion Methods

    }


}