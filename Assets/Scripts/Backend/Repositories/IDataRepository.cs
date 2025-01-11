using System;
using System.Collections.Generic;

namespace ProjectExodus.Backend.Repositories
{

    public interface IDataRepository<TEntity> where TEntity : class
    {

        #region - - - - - - Methods - - - - - -

        void Create(TEntity entityToCreate);

        void Delete(Guid idToDelete);

        IEnumerable<TEntity> GetEntities();

        void Update(Guid ID, TEntity entityToUpdate);
        
        #endregion Methods

    }

}