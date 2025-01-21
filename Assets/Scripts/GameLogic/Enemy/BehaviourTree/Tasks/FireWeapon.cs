using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Fire Weapon")]
    public class FireWeapon : Leaf
    {
        public ActionReference WeaponHandler = new();
        
        public override NodeResult Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}