using System;
using System.Collections.Generic;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip
{

    public class CreateShipInputPort
    {

        #region - - - - - - Properties - - - - - -

        public int AssetID { get; set; }
        
        public float PlatingHealthModifier { get; set; }

        public float ShieldHealthModifier { get; set; }

        public List<Guid> Weapons { get; set; }

        #endregion properties
  
    }

}