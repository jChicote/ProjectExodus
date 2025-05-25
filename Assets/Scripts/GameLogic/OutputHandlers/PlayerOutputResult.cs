using ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer;
using ProjectExodus.Domain.Models;
using UnityEngine;

namespace ProjectExodus.GameLogic.OutputHandlers
{
    
    public class CreatePlayerOutputResult : ICreatePlayerOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public PlayerModel Result { get; set; }
        
        public bool IsSuccessful { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        void ICreatePlayerOutputPort.PresentCreatedPlayer(PlayerModel player)
        {
            this.Result = player;
            this.IsSuccessful = true;
        }

        void ICreatePlayerOutputPort.PresentUnsuccessfulCreationOfPlayer()
            => this.IsSuccessful = false;

        #endregion Methods
  
    }
    
    public class PlayerOutputResult : IGetPlayerOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public PlayerModel Result;
        public bool IsSuccessful;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void IGetPlayerOutputPort.PresentPlayer(PlayerModel player)
        {
            this.Result = player;
            this.IsSuccessful = true;
        }

        void IGetPlayerOutputPort.PresentPlayerNotFound()
        {
            Debug.Log("[ERROR]: Player is not found in file.");
            this.IsSuccessful = false;
        }

        #endregion Methods
  
    }
    
    public class UpdatePlayerOutputResult : IUpdatePlayerOutputPort
    {

        #region - - - - - - Methods - - - - - -
        
        void IUpdatePlayerOutputPort.PresentSuccessfulUpdate()
        {
        }

        #endregion Methods
  
    }

}