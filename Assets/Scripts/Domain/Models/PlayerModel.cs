using System;
using System.Collections.Generic;
using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Domain.Models
{

    public class PlayerModel
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;

        public List<Ship> Ships = new();

        #endregion Fields

    }

}