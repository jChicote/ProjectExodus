using System;
using ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.DebugSupport.Presenters
{

    public class DebugCreatePlayerOutputHandler : ICreatePlayerOutputPort
    {

        #region - - - - - - Fields - - - - - -

        public PlayerModel Result;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public void PresentCreatedPlayer(PlayerModel player)
            => this.Result = player;

        public void PresentUnsuccessfulCreationOfPlayer()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
  
    }

}