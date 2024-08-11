using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectExodus.Backend.JsonDataContext
{

    public interface IDataContext
    {

        #region - - - - - - Methods - - - - - -

        void Add<TEntity>(TEntity newObject) where TEntity : class;
        
        ICollection<TEntity> GetEntities<TEntity>() where TEntity : class;

        Task Load();

        void Remove<TEntity>(TEntity objectToRemove) where TEntity : class;

        void Update<TEntity>(Guid ID, TEntity objectToUpdate) where TEntity : class;

        Task SaveChanges();

        #endregion Methods

    }

}