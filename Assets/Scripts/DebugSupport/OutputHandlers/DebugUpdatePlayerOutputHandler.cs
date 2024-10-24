using ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer;
using ProjectExodus.Domain.Models;
using UnityEngine;

namespace ProjectExodus.DebugSupport.OutputHandlers
{

    public class DebugUpdatePlayerOutputHandler : IUpdatePlayerOutputPort
    {

        #region - - - - - - Fields - - - - - -

        public PlayerModel Result;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public void PresentCreatedPlayer(PlayerModel player)
            => this.Result = player;

        public void PresentSuccessfulUpdate() 
            => Debug.Log("[LOG]: Successfully updated the Player");

        #endregion Methods

    }

}