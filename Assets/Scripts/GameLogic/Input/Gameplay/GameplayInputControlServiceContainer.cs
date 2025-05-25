using ProjectExodus.GameLogic.Player.Movement;
using ProjectExodus.GameLogic.Player.PlayerTargetingSystem;
using ProjectExodus.GameLogic.Player.Weapons;
using UnityEngine;

namespace ProjectExodus.GameLogic.Input.Gameplay
{

    public class GameplayInputControlServiceContainer
    {

        #region - - - - - - Properties - - - - - -

        public IPlayerMovement PlayerMovement { get; set; }
        
        public IPlayerTargetingSystem PlayerTargetingSystem { get; set; }
        
        public IPlayerWeaponSystems PlayerWeaponSystems { get; set; }
        
        public IDebugHandler DebugHandler { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void SetNewPlayerComponents(GameObject newPlayer)
        {
            this.PlayerMovement = newPlayer.GetComponent<IPlayerMovement>();
            this.PlayerTargetingSystem = newPlayer.GetComponent<IPlayerTargetingSystem>();
            this.PlayerWeaponSystems = newPlayer.GetComponent<IPlayerWeaponSystems>();
        }

        #endregion Methods
  
    }

}