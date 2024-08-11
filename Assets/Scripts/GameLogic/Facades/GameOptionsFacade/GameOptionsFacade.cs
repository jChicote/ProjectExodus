using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions;
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
        private IDataRepository m_DataRepository;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameOptionsFacade(IDataRepository dataRepository, GameSettings.GameSettings gameSettings) 
            => this.m_GetInteractor = new GetGameOptionsInteractor(dataRepository, gameSettings);

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
            // TODO: Change the gameOptions to be a DTO exiting the Backend.
            // TODO: Change the Models to be in backend. Additionally the models in game logic
            //      are only containers not persisted to the JSON.
            this.m_GameSettings.SetGameOptions(gameOptions);
        }

        #endregion Methods
        
    }

}