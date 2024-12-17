using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerTargetingSystem
{

    public interface IPlayerTargetingSystem
    {
        
        #region - - - - - - Initializers - - - - - -

        void Initialize(UnityEngine.Camera camera);

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void ConfirmTargetLock();

        void SearchForTarget(Vector2 screenPosition);

        #endregion Methods

    }

}