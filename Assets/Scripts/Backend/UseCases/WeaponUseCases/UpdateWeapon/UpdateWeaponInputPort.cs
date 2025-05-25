using System;

namespace ProjectExodus.Backend.UseCases.WeaponUseCases.UpdateWeapon
{

    public class UpdateWeaponInputPort
    {

        #region - - - - - - Properties - - - - - -
        
        public Guid ID { get; set; }

        public int AssignedBayID { get; set; }

        #endregion Properties
  
    }

}