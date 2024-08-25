using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Settings;

namespace ProjectExodus.GameLogic.GameStartup
{

    public class GameSetupConfig
    {

        #region - - - - - - Properties - - - - - -

        // --------------------------------
        // Game Services
        // --------------------------------
        
        public IDataContext DataContext { get; private set; }
        
        public GameSettings GameSettings { get; private set; }
        
        public IObjectMapper ObjectMapper { get; private set; }
        
        // --------------------------------
        // Data Repositories
        // --------------------------------
        
        public IDataRepository<GameOptions> GameOptionsRepository { get; private set; }
        
        // --------------------------------
        // UseCase Facades
        // --------------------------------
        
        public GameOptionsFacade GameOptionsFacade { get; private set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void SetupGameServices(
            IDataContext dataContext,
            GameSettings gameSettings,
            IObjectMapper objectMapper)
        {
            this.DataContext = dataContext;
            this.GameSettings = gameSettings;
            this.ObjectMapper = objectMapper;
        }

        public void SetupRepositories(IDataRepository<GameOptions> gameOptionsRepository) 
            => this.GameOptionsRepository = gameOptionsRepository;

        public void SetupUseCaseFacades(GameOptionsFacade gameOptionsFacade) 
            => this.GameOptionsFacade = gameOptionsFacade;

        #endregion Methods
          
    }

}