using System;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer;

namespace ProjectExodus.GameLogic.Facades.PlayerControllers
{

    public class PlayerActionFacade : IPlayerActionFacade
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInteractor<CreatePlayerInputPort, ICreatePlayerOutputPort> m_CreatePlayerInteractor;
        private readonly IUseCaseInteractor<GetPlayerInputPort, IGetPlayerOutputPort> m_GetPlayerInteractor;
        private readonly IUseCaseInteractor<UpdatePlayerInputPort, IUpdatePlayerOutputPort> m_UpdateInteractor;
        
        #endregion Fields

        #region - - - - - - Controllers - - - - - -

        public PlayerActionFacade(
            IUseCaseInteractor<CreatePlayerInputPort, ICreatePlayerOutputPort> createPlayerInteractor,
            IUseCaseInteractor<GetPlayerInputPort, IGetPlayerOutputPort> getPlayerInteractor,
            IUseCaseInteractor<UpdatePlayerInputPort, IUpdatePlayerOutputPort> updatePlayerInteractor)
        {
            this.m_CreatePlayerInteractor = 
                createPlayerInteractor ?? throw new ArgumentNullException(nameof(createPlayerInteractor));
            this.m_GetPlayerInteractor =
                getPlayerInteractor ?? throw new ArgumentNullException(nameof(getPlayerInteractor));
            this.m_UpdateInteractor =
                updatePlayerInteractor ?? throw new ArgumentNullException(nameof(updatePlayerInteractor));
        }

        #endregion Controllers

        #region - - - - - - Create Player Methods - - - - - -

        void IPlayerActionFacade.CreatePlayer(CreatePlayerInputPort inputPort, ICreatePlayerOutputPort outputPort) 
            => this.m_CreatePlayerInteractor.Handle(inputPort, outputPort);

        #endregion Create Player Methods

        #region - - - - - - Get Player Methods - - - - - -

        void IPlayerActionFacade.GetPlayer(GetPlayerInputPort inputPort, IGetPlayerOutputPort outputPort) 
            => this.m_GetPlayerInteractor.Handle(inputPort, outputPort);

        #endregion Get Player Methods

        #region - - - - - - Update Player Methods - - - - - -

        void IPlayerActionFacade.UpdatePlayer(UpdatePlayerInputPort inputPort, IUpdatePlayerOutputPort outputPort)
            => this.m_UpdateInteractor.Handle(inputPort, outputPort);

        #endregion Update Player Methods

    }

}