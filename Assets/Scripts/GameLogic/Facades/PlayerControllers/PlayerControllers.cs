using System;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;

namespace ProjectExodus.GameLogic.Facades.PlayerControllers
{

    public class PlayerControllers : IPlayerControllers
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInteractor<CreatePlayerInputPort, ICreatePlayerOutputPort> m_CreatePlayerInteractor;
        private readonly IUseCaseInteractor<GetPlayerInputPort, IGetPlayerOutputPort> m_GetPlayerInteractor;

        #endregion Fields

        #region - - - - - - Controllers - - - - - -

        public PlayerControllers(
            IUseCaseInteractor<CreatePlayerInputPort, ICreatePlayerOutputPort> createPlayerInteractor,
            IUseCaseInteractor<GetPlayerInputPort, IGetPlayerOutputPort> getPlayerInteractor)

        {
            this.m_CreatePlayerInteractor = 
                createPlayerInteractor ?? throw new ArgumentNullException(nameof(createPlayerInteractor));
            this.m_GetPlayerInteractor =
                getPlayerInteractor ?? throw new ArgumentNullException(nameof(getPlayerInteractor));
        }

        #endregion Controllers

        #region - - - - - - Create Player Methods - - - - - -

        void IPlayerControllers.CreatePlayer(CreatePlayerInputPort inputPort, ICreatePlayerOutputPort outputPort) 
            => this.m_CreatePlayerInteractor.Handle(inputPort, outputPort);

        #endregion Create Player Methods

        #region - - - - - - Get Player Methods - - - - - -

        void IPlayerControllers.GetPlayer(GetPlayerInputPort inputPort, IGetPlayerOutputPort outputPort) 
            => this.m_GetPlayerInteractor.Handle(inputPort, outputPort);

        #endregion Get Player Methods

    }

}