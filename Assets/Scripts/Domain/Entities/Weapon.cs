using System;

namespace ProjectExodus.Domain.Entities
{

    [Serializable]
    public class Weapon
    {

        #region - - - - - - Fields - - - - - -

        public int ID;

        public int AssignedBayID;

        public float FireRateModifier;

        public float ReloadPeriodModifier;

        public float AmmoSizeModifier;
        
        #endregion Fields

    }

}