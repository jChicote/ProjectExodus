using System;
using System.Collections.Generic;

namespace ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer
{

    public class UpdatePlayerInputPort
    {

        #region - - - - - - Properties - - - - - -

        public Guid PlayerID { get; set; }

        public List<Guid> Ships { get; set; }

        #endregion Properties

    }

}