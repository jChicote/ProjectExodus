using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Start Firing Weapons")]
    public class StartFiringWeapons : Leaf
    {

        #region - - - - - - Fields - - - - - -

        public WeaponSystemsInfoReference WeaponSystemsInfo = new();

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public override NodeResult Execute()
        {
            if (this.WeaponSystemsInfo.Value.IsFiring) return NodeResult.success;
            
            this.WeaponSystemsInfo.Value.EnableWeaponFireAction?.Invoke();
            return NodeResult.success;
        }

        #endregion Methods

    }

}