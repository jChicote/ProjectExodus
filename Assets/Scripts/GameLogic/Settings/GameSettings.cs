using System;
using ProjectExodus.Domain.Models;
using UnityEngine;

namespace ProjectExodus.GameLogic.Settings
{

    /// <summary>
    /// Holds configurations and data to the setup of the game.
    /// </summary>
    [Serializable]
    public class GameSettings
    {

        #region - - - - - - Fields - - - - - -

        public GameOptionsModel GameOptionsModel { get; private set; }
        
        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void SetGameOptions(GameOptionsModel gameOptionsModel)
        {
            if (this.GameOptionsModel != null)
                Debug.LogWarning("You are overriding pre-existing GameOptions.");
            
            this.GameOptionsModel = gameOptionsModel;
        }

        #endregion Methods
  
    }

}