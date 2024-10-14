using System;
using System.Collections.Generic;

namespace ProjectExodus.Domain.Entities
{

    [Serializable]
    public class Ship
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public List<Guid> Weapons;

        #endregion Fields

    }

}