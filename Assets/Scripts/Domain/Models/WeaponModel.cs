using System;

namespace ProjectExodus.Domain.Models
{

    public class WeaponModel
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public int AssetID;

        public int AssignedBayID;

        public int AmmoSizeModifier; 
        
        public float FireRateModifier;

        public float ReloadPeriodModifier;

        #endregion Fields

    }

}