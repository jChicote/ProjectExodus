using System;

namespace ProjectExodus.Domain.Models
{

    public class WeaponModel
    {

        #region - - - - - - Properties - - - - - -

        public Guid ID { get; set; }

        public int AssetID { get; set; }

        public int AssignedBayID { get; set; }

        public int AmmoSizeModifier { get; set; }
        
        public float FireRateModifier { get; set; }

        public float ReloadPeriodModifier { get; set; }

        #endregion Properties

    }

}