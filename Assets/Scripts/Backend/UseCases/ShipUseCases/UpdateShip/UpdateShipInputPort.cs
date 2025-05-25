using System;
using System.Collections.Generic;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip
{

    public class UpdateShipInputPort
    {

        #region - - - - - - Properties - - - - - -

        public Guid ID { get; set; }
                
        public float PlatingHealthModifier { get; set; }

        public float ShieldHealthModifier { get; set; }

        public List<Guid> Weapons { get; set; }

        #endregion Properties
  
    }

}