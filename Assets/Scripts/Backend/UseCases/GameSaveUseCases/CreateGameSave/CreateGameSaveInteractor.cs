using System;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave
{

    public class CreateGameSaveInteractor : 
        IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapper m_Mapper;
        private readonly IDataRepository<GameSave> m_Repository;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateGameSaveInteractor(IObjectMapper objectMapper, IDataRepository<GameSave> repository)
        {
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this.m_Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort>.Handle(
            CreateGameSaveInputPort inputPort, 
            ICreateGameSaveOutputPort outputPort)
        {
            GameSave _GameSave = new GameSave();
            this.m_Mapper.Map(inputPort, _GameSave);
            this.m_Repository.Create(_GameSave);

            GameSaveModel _GameSaveModel = new GameSaveModel();
            this.m_Mapper.Map(_GameSave, _GameSaveModel);
            
            outputPort.PresentCreatedGameSave(_GameSaveModel);
        }

        #endregion Methods
  
    }

}