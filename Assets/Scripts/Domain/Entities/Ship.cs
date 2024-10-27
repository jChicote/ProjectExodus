using System;
using System.Collections.Generic;

namespace ProjectExodus.Domain.Entities
{

    [Serializable]
    public class Ship
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public int AssetID;

        public float PlatingHealthModifier;

        public float ShieldHealthModifier;

        public List<Guid> Weapons;

        #endregion Fields

    }

}