using ProjectExodus.UserInterface;
using UnityEngine;

namespace ProjectExodus
{

    public interface IWeaponTrackingHUDController : IScreenStateController
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize();

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void SetTargetCrosshairPosition(Vector2 screenPosition);

        #endregion Methods

    }


}