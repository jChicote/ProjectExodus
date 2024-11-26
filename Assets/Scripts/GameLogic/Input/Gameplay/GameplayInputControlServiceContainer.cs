using ProjectExodus.GameLogic.Player.Movement;
using ProjectExodus.GameLogic.Player.PlayerTargetingSystem;
using ProjectExodus.GameLogic.Player.Weapons;

namespace ProjectExodus.GameLogic.Input.Gameplay
{

    public class GameplayInputControlServiceContainer
    {

        #region - - - - - - Properties - - - - - -

        public IPlayerMovement PlayerMovement { get; set; }
        
        public IPlayerTargetingSystem PlayerTargetingSystem { get; set; }
        
        public IPlayerWeaponSystems PlayerWeaponSystems { get; set; }

        #endregion Properties
  
    }

}