using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.Entities;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.Repositories.GameOptionsRepository
{

    public class GameOptionsRepository : IDataRepository
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;
        private readonly IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameOptionsRepository(IDataContext dataContext, IObjectMapper mapper)
        {
            this.m_DataContext = dataContext;
            this.m_Mapper = mapper;
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IDataRepository.Create<GameOptions>(GameOptions entityToCreate)
        {
            var _GameOptionsEntity = new GameOptionsEntity();
            this.m_Mapper.Map(entityToCreate, _GameOptionsEntity);
            this.m_DataContext.Add(_GameOptionsEntity);
        }

        IEnumerable<GameOptions> IDataRepository.Get<GameOptions>()
            => this.m_DataContext
                .GetEntities<GameOptionsEntity>()
                .Select(goe =>
                {
                    GameOptions _GameOptions = new GameOptions();
                    this.m_Mapper.Map(goe, _GameOptions);
                    return _GameOptions;
                });

        void IDataRepository.Update<GameOptions>(Guid ID, GameOptions entityToUpdate)
        {
            var _GameOptionsEntity = new GameOptionsEntity();
            this.m_Mapper.Map(entityToUpdate, _GameOptionsEntity);
            this.m_DataContext.Update(ID, entityToUpdate);
        }

        #endregion Methods

    }

}