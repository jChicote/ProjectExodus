using System;
using System.Collections.Generic;

namespace ProjectExodus.Domain.Entities
{

    [Serializable]
    public class Player
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public Guid GameSaveID;

        public List<Guid> Ships;
        
        #endregion Fields

    }

}