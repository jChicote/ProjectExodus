using System;
using System.Linq;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves
{

    public class GetGameSavesInteractor : 
        IUseCaseInteractor<GetGameSavesInputPort, IGetGameSavesOutputPort>
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapper m_Mapper;
        private readonly IDataRepository<GameSave> m_Repository;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GetGameSavesInteractor(IObjectMapper mapper, IDataRepository<GameSave> dataRepository)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_Repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<GetGameSavesInputPort, IGetGameSavesOutputPort>.Handle(
            GetGameSavesInputPort inputPort, 
            IGetGameSavesOutputPort outputPort)
        {
            var _GameSaves = this.m_Repository
                .GetEntities()
                .Select(gs => this.m_Mapper.Map(gs, new GameSaveModel()))
                .ToList();
            
            outputPort.PresentGameSaves(_GameSaves);
        }
        
        #endregion Methods
        
    }

}