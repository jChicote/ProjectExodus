using System;
using System.Collections.Generic;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Backend.Repositories.GameSaveRepository
{

    public class GameSaveRepository : IDataRepository<GameSave>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameSaveRepository(IDataContext dataContext) 
            => this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IDataRepository<GameSave>.Create(GameSave entityToCreate)
            => this.m_DataContext.Add(entityToCreate);

        void IDataRepository<GameSave>.Delete(Guid id)
            => this.m_DataContext.Delete<GameSave>(id);

        IEnumerable<GameSave> IDataRepository<GameSave>.GetEntities()
            => this.m_DataContext.GetEntities<GameSave>();

        void IDataRepository<GameSave>.Update(Guid ID, GameSave entityToUpdate) 
            => this.m_DataContext.Update(ID, entityToUpdate);

        #endregion Methods
  
    }

}