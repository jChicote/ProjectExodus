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

        /// <summary>
        /// Loads all the data from the storage file / media.
        /// </summary>
        /// <remarks>Invoking this method replaces any instance of the GameData.</remarks>
        Task Load();

        void Delete<TEntity>(Guid id) where TEntity : class;

        void Update<TEntity>(Guid ID, TEntity objectToUpdate) where TEntity : class;

        /// <summary>
        /// Persists current state of changes to the storage file / media.
        /// </summary>
        Task SaveChanges();

        #endregion Methods

    }

}