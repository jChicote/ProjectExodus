using System;
using System.Collections.Generic;

namespace ProjectExodus.Domain.Models
{

    public class ShipModel
    {

        #region - - - - - - Properties - - - - - -

        public Guid ID { get; set; }

        public int AssetID { get; set; }
        
        public float PlatingHealthModifier { get; set; }

        public float ShieldHealthModifier { get; set; }

        public List<WeaponModel> Weapons { get; set; }

        #endregion Properties
  
    }

}