using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerTargetingSystem
{

    public interface IPlayerTargetingSystem
    {
        
        #region - - - - - - Initializers - - - - - -

        void Initialize(UnityEngine.Camera camera, IPlayerTargetingHUDController playerTargetingHUDController);

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void ActivateTargeting();

        void DeactivateTargeting();

        void ConfirmTargetLock();

        void SearchForTarget(Vector2 screenPosition);

        #endregion Methods

    }

}