using ProjectExodus.Backend.Entities;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameOptions.CreateGameOptions;
using ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions;
using ProjectExodus.Backend.UseCases.GameOptions.UpdateGameOptions;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;
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

        private readonly GameSettings.GameSettings m_GameSettings;
        private readonly IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameOptionsFacade(
            IDataRepository<GameOptions> repository, 
            GameSettings.GameSettings gameSettings, 
            IObjectMapper mapper)
        {
            this.m_GameSettings = gameSettings;
            this.m_Mapper = mapper;

            this.m_CreateInteractor = new CreateGameOptionsInteractor(mapper, repository);
            this.m_GetInteractor = new GetGameOptionsInteractor(repository);
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        // -------------------------------------
        // Create Game Options
        // -------------------------------------
        
        void IGameOptionsFacade.CreateGameOptions()
        {
            // Create with default starting values
            // TODO: Use scriptable object default values
            this.m_CreateInteractor.Handle(new CreateGameOptionsInputPort(), this);
        }

        void ICreateGameOptionsOutputPort.PresentCreatedGameOptions(GameOptions gameOptions)
        {
            var _GameOptionsModel = new GameOptionsModel();
            this.m_Mapper.Map(gameOptions, _GameOptionsModel);
            this.m_GameSettings.SetGameOptions(_GameOptionsModel);

            Debug.Log("New GameOptions has been created.");
        }
        
        // -------------------------------------
        // Update Game Options
        // -------------------------------------

        void IGameOptionsFacade.UpdateGameOptions()
        {
            // Update the GameLogic's Model
            // Update the Entity tracked.
            UpdateGameOptionsInputPort _UpdateGameOptionsInputPort = new UpdateGameOptionsInputPort();
            this.m_UpdateInteractor.Handle(_UpdateGameOptionsInputPort, this);
        }

        void IUpdateOptionsOutputPort.PresentSuccessfulUpdate()
        {
            Debug.Log("Game Options have been updated");
        }
        
        // -------------------------------------
        // Get Game Options
        // -------------------------------------

        void IGameOptionsFacade.GetGameOptions() 
            => this.m_GetInteractor.Handle(new GetGameOptionsInputPort(), this);

        void IGetGameOptionsOutputPort.PresentGameOptions(GameOptions gameOptions)
        {
            var _GameOptionsModel = new GameOptionsModel();
            this.m_Mapper.Map(gameOptions, _GameOptionsModel);
            this.m_GameSettings.SetGameOptions(_GameOptionsModel);
        }

        #endregion Methods
        
    }

}