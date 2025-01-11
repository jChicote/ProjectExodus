using System;

namespace ProjectExodus.Domain.Entities
{

    [Serializable]
    public class Weapon
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public int AssetID;

        public int AmmoSizeModifier;

        public int AssignedBayID;

        public float FireRateModifier;

        public float ReloadPeriodModifier;
        
        #endregion Fields

    }

}