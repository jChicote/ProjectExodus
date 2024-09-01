using System;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;

namespace ProjectExodus.GameLogic.Facades.GameSaveFacade
{

    public class GameSaveFacade : IGameSaveFacade
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort> m_CreateInteractor;
        private readonly IUseCaseInteractor<GetGameSavesInputPort, IGetGameSavesOutputPort> m_GetInteractor;
        private readonly IUseCaseInteractor<UpdateGameSaveInputPort, IUpdateGameSaveOutputPort> m_UpdateInteractor;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GameSaveFacade(
            IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort> createInteractor,
            IUseCaseInteractor<GetGameSavesInputPort, IGetGameSavesOutputPort> getInteractor,
            IUseCaseInteractor<UpdateGameSaveInputPort, IUpdateGameSaveOutputPort> updateInteractor)
        {
            this.m_CreateInteractor = createInteractor ?? throw new ArgumentNullException(nameof(createInteractor));
            this.m_GetInteractor = getInteractor ?? throw new ArgumentNullException(nameof(getInteractor));
            this.m_UpdateInteractor = updateInteractor ?? throw new ArgumentNullException(nameof(updateInteractor));
        }

        #endregion Constructors
  
        #region - - - - - - Create GameSave Methods - - - - - -

        void IGameSaveFacade.CreateGameSave(CreateGameSaveInputPort inputPort, ICreateGameSaveOutputPort outputPort) 
            => this.m_CreateInteractor.Handle(inputPort, outputPort);

        #endregion Create GameSave Methods

        #region - - - - - - Get GameSave Methods - - - - - -

        void IGameSaveFacade.GetGameSaves(IGetGameSavesOutputPort outputPort) 
            => this.m_GetInteractor.Handle(new GetGameSavesInputPort(), outputPort);

        #endregion Get GameSave Methods

        #region - - - - - - Update GameSave Methods - - - - - -

        void IGameSaveFacade.UpdateGameSave(UpdateGameSaveInputPort inputPort, IUpdateGameSaveOutputPort outputPort) 
            => this.m_UpdateInteractor.Handle(inputPort, outputPort);

        #endregion Update GameSave Methods
  
  
    }

}