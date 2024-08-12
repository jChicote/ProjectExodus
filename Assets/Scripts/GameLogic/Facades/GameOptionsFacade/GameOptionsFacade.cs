using ProjectExodus.Backend.Entities;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;

namespace ProjectExodus.GameLogic.Facades.GameOptionsFacade
{

    public class GameOptionsFacade : 
        IGameOptionsFacade,
        IGetGameOptionsOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort> m_GetInteractor;

        private GameSettings.GameSettings m_GameSettings;
        private IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameOptionsFacade(
            IDataRepository<GameOptions> dataRepository, 
            GameSettings.GameSettings gameSettings, 
            IObjectMapper mapper)
        {
            this.m_GameSettings = gameSettings;
            this.m_Mapper = mapper;
            this.m_GetInteractor = new GetGameOptionsInteractor(dataRepository);
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IGameOptionsFacade.CreateGameOptions()
        {
            // Create the GameLogic's Model
            // Create the Entity tracked.
            
            throw new System.NotImplementedException();
        }

        void IGameOptionsFacade.GetGameOption() 
            => this.m_GetInteractor.Handle(new GetGameOptionsInputPort(), this);

        void IGameOptionsFacade.UpdateGameOptions()
        {
            // Update the GameLogic's Model
            // Update the Entity tracked.
            
            throw new System.NotImplementedException();
        }

        void IGetGameOptionsOutputPort.PresentGameOptions(GameOptions gameOptions)
        {
            var _GameOptionsModel = new GameOptionsModel();
            this.m_Mapper.Map(gameOptions, _GameOptionsModel);
            this.m_GameSettings.SetGameOptions(_GameOptionsModel);
        }

        #endregion Methods
        
    }

}