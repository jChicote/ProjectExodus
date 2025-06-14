using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.CreateGameOptions;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.GetGameOptions;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.UpdateGameOptions;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Settings;
using UnityEngine;

namespace ProjectExodus.GameLogic.Facades.GameOptionsFacade
{

    public class GameOptionsFacade : 
        ICreateGameOptionsOutputPort,
        IGameOptionsFacade,
        IGetGameOptionsOutputPort,
        IUpdateOptionsOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInteractor<CreateGameOptionsInputPort, ICreateGameOptionsOutputPort> m_CreateInteractor;
        private readonly IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort> m_GetInteractor;
        private readonly IUseCaseInteractor<UpdateGameOptionsInputPort, IUpdateOptionsOutputPort> m_UpdateInteractor;

        private readonly GameSettings m_GameSettings;
        private readonly IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameOptionsFacade(
            IDataRepository<GameOptions> repository, 
            GameSettings gameSettings, 
            IObjectMapper mapper)
        {
            this.m_GameSettings = gameSettings;
            this.m_Mapper = mapper;

            this.m_CreateInteractor = new CreateGameOptionsInteractor(mapper, repository);
            this.m_GetInteractor = new GetGameOptionsInteractor(repository);
            this.m_UpdateInteractor = new UpdateGameOptionsInteractor(mapper, repository);
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        // -------------------------------------
        // Create Game Options
        // -------------------------------------
        
        void IGameOptionsFacade.CreateGameOptions() 
            => this.m_CreateInteractor.Handle(new CreateGameOptionsInputPort(), this);

        void ICreateGameOptionsOutputPort.PresentCreatedGameOptions(GameOptions gameOptions)
        {
            var _GameOptionsModel = new GameOptionsModel();
            this.m_Mapper.Map(gameOptions, _GameOptionsModel);
            this.m_GameSettings.SetGameOptions(_GameOptionsModel);
        }
        
        // -------------------------------------
        // Update Game Options
        // -------------------------------------

        void IGameOptionsFacade.UpdateGameOptions(UpdateGameOptionsInputPort inputPort) 
            => this.m_UpdateInteractor.Handle(inputPort, this);

        void IUpdateOptionsOutputPort.PresentSuccessfulUpdate(GameOptions gameOptions) 
            => this.m_Mapper.Map(gameOptions, this.m_GameSettings.GameOptionsModel);

        void IUpdateOptionsOutputPort.PresentFailedUpdateOfGameOptions() 
            => Debug.Log("[UPDATE FAIL]: Failed to update the existing Gaming options");

        // -------------------------------------
        // Get Game Options
        // -------------------------------------

        void IGameOptionsFacade.GetGameOptions() 
            => this.m_GetInteractor.Handle(new GetGameOptionsInputPort(), this);

        void IGetGameOptionsOutputPort.PresentGameOptions(IEnumerable<GameOptions> gameOptions)
        {
            if (gameOptions == null || !gameOptions.Any())
            {
                Debug.LogWarning("[WARNING]: No game options were found.");
                return;
            }
            
            var _GameOptionsModel = new GameOptionsModel();
            this.m_Mapper.Map(gameOptions.First(), _GameOptionsModel);
            this.m_GameSettings.SetGameOptions(_GameOptionsModel);
        }

        #endregion Methods
        
    }

}