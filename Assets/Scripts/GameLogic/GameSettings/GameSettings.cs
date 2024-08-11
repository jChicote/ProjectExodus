using System;
using ProjectExodus.GameLogic.Models;
using UnityEngine;

namespace ProjectExodus.GameLogic.GameSettings
{

    /// <summary>
    /// Holds configurations and data to the setup of the game.
    /// </summary>
    [Serializable]
    public class GameSettings
    {

        #region - - - - - - Fields - - - - - -

        public GameOptions GameOptions { get; private set; }

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void SetGameOptions(GameOptions gameOptions)
        {
            if (this.GameOptions != null)
                Debug.LogWarning("You are overriding pre-existing GameOptions.");
            
            this.GameOptions = gameOptions;
        }

        #endregion Methods
  
  
    }

}