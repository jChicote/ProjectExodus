using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Stop Firing Weapons")]
    public class EndFiringWeapons : Leaf
    {
        
        #region - - - - - - Fields - - - - - -

        public WeaponSystemsInfoReference WeaponSystemsInfo = new();

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public override NodeResult Execute()
        {
            if (!this.WeaponSystemsInfo.Value.IsFiring) return NodeResult.success;
            
            this.WeaponSystemsInfo.Value.DisableWeaponFireAction?.Invoke();
            return NodeResult.success;
        }

        #endregion Methods
        
    }

}