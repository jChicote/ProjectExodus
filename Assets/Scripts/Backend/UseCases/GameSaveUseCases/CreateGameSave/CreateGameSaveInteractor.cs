using System;
using ProjectExodus.Backend.JsonDataContext;
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

        private readonly IDataContext m_DataContext;
        private readonly IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateGameSaveInteractor(
            IDataContext dataContext, 
            IObjectMapper objectMapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort>.Handle(
            CreateGameSaveInputPort inputPort, 
            ICreateGameSaveOutputPort outputPort)
        {
            GameSave _GameSave = new GameSave();
            this.m_Mapper.Map(inputPort, _GameSave);
            this.m_DataContext.Add(_GameSave);

            outputPort.PresentCreatedGameSave(this.m_Mapper.Map(_GameSave, new GameSaveModel()));
        }

        #endregion Methods
  
    }

}