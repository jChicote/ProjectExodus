using System;
using System.Collections.Generic;

namespace ProjectExodus.Domain.Models
{

    public class ShipModel
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public int AssetID;

        public List<WeaponModel> Weapons;

        #endregion Fields
  
    }

}