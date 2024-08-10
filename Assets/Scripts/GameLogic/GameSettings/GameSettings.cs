using System;
using ProjectExodus.GameLogic.Models;

namespace ProjectExodus.GameLogic.GameSettings
{

    /// <summary>
    /// Holds configurations and data to the setup of the game.
    /// </summary>
    [Serializable]
    public class GameSettings
    {

        #region - - - - - - Fields - - - - - -

        public GameOptions GameOptions { get; set; }

        #endregion Fields
  
    }

}