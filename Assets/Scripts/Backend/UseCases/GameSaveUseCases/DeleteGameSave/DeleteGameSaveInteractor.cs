using System;
using System.Data;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.DeleteGameSave
{

    public class DeleteGameSaveInteractor : 
        IUseCaseInteractor<DeleteGameSaveInputPort, IDeleteGameSaveOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataRepository<GameSave> m_DataRepository;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DeleteGameSaveInteractor(IDataRepository<GameSave> dataRepository)
            => this.m_DataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
            
        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<DeleteGameSaveInputPort, IDeleteGameSaveOutputPort>.Handle(
            DeleteGameSaveInputPort inputPort, 
            IDeleteGameSaveOutputPort outputPort)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
  
    }

}