using System;
using System.Linq;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave
{

    public class UpdateGameSaveInteractor :
        IUseCaseInteractor<UpdateGameSaveInputPort, IUpdateGameSaveOutputPort>
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IDataRepository<GameSave> m_Repository;
        private readonly IObjectMapper m_Mapper;
        
        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public UpdateGameSaveInteractor(
            IObjectMapper mapper,
            IDataRepository<GameSave> repository)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<UpdateGameSaveInputPort, IUpdateGameSaveOutputPort>.Handle(
            UpdateGameSaveInputPort inputPort, 
            IUpdateGameSaveOutputPort outputPort)
        {
            var _GameSave = this.m_Repository.GetEntities().FirstOrDefault(gs => gs.ID == inputPort.ID);
            if (_GameSave == null)
            {
                outputPort.PresentFailedUpdateOfGameSave();
                return;
            }
            
            this.m_Mapper.Map(inputPort, _GameSave);
            this.m_Repository.Update(inputPort.ID, _GameSave);
            
            outputPort.PresentUpdatedGameSave(this.m_Mapper.Map(_GameSave, new GameSaveModel()));
        }
        
        #endregion Methods

    }

}