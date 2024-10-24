using System;
using System.Collections.Generic;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Backend.Repositories.WeaponRepository
{

    public class WeaponRepository : IDataRepository<Weapon>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;

        #endregion Fields
        
        #region - - - - - - Constructors - - - - - -

        public WeaponRepository(IDataContext dataContext) 
            => this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        #endregion Constructors
        
        #region - - - - - - Methods - - - - - -

        void IDataRepository<Weapon>.Create(Weapon entityToCreate)
        {
            throw new NotImplementedException();
        }

        void IDataRepository<Weapon>.Delete(Guid idToDelete)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Weapon> IDataRepository<Weapon>.GetEntities() 
            => this.m_DataContext.GetEntities<Weapon>();

        void IDataRepository<Weapon>.Update(Guid ID, Weapon entityToUpdate)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
  
    }

}