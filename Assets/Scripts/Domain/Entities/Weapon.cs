using System;
using UnityEngine.Serialization;

namespace ProjectExodus.Domain.Entities
{

    [Serializable]
    public class Weapon
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public int AssetID;

        public int AssignedBayID;

        public float FireRateModifier;

        public float ReloadPeriodModifier;

        public float AmmoSizeModifier;
        
        #endregion Fields

    }

}