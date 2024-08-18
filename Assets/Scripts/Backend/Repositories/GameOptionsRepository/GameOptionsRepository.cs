using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.Entities;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.Repositories.GameOptionsRepository
{

    public class GameOptionsRepository : IDataRepository<GameOptions>
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

        void IDataRepository<GameOptions>.Create(GameOptions entityToCreate) 
            => this.m_DataContext.Add(entityToCreate);

        IEnumerable<GameOptions> IDataRepository<GameOptions>.GetEntities()
            => this.m_DataContext
                .GetEntities<GameOptions>()
                .Select(go =>
                {
                    GameOptions _GameOptions = new GameOptions();
                    this.m_Mapper.Map(go, _GameOptions);
                    return _GameOptions;
                });

        void IDataRepository<GameOptions>.Update(Guid ID, GameOptions entityToUpdate)
        {
            var _GameOptions = new GameOptions();
            this.m_Mapper.Map(entityToUpdate, _GameOptions);
            this.m_DataContext.Update(ID, entityToUpdate);
        }

        #endregion Methods

    }

}