using System;
using System.Collections.Generic;
using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Domain.Models
{

    public class PlayerModel
    {

        #region - - - - - - Properties - - - - - -

        public Guid ID { get; set; }

        public List<Ship> Ships { get; set; } = new();

        #endregion Properties

    }

}