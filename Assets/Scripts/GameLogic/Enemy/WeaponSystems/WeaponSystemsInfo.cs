using System;
using UnityEngine;

namespace ProjectExodus
{

    public class WeaponSystemsInfo : MonoBehaviour
    {

        #region - - - - - - Properties - - - - - -

        public float FireRate { get; set; }
        
        public float FiringViewArc { get; set; }

        public bool IsFiring { get; set; }

        public Action EnableWeaponFireAction { get; set; }
        
        public Action DisableWeaponFireAction { get; set; }
        
        #endregion Properties
        
    }

}