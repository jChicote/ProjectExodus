using System;
using System.Collections.Generic;

namespace ProjectExodus.Backend.Repositories
{

    public interface IDataRepository
    {

        #region - - - - - - Methods - - - - - -

        void Create<TEntity>(TEntity entityToCreate) where TEntity : class;

        IEnumerable<TEntity> Get<TEntity>() where TEntity : class, new();

        void Update<TEntity>(Guid ID, TEntity entityToUpdate) where TEntity : class;
        
        #endregion Methods

    }

}