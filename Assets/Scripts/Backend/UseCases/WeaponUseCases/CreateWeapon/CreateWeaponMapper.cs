using System;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon
{

    public class CreateWeaponMapper
    {

        #region - - - - - - Constructors - - - - - -

        public CreateWeaponMapper(IObjectMapperRegister mapperRegister) 
            => mapperRegister.AddMappingAction<CreateWeaponInputPort, Weapon>(
                this.MapCreateWeaponInputPortToWeaponEntity);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapCreateWeaponInputPortToWeaponEntity(CreateWeaponInputPort source, Weapon destination)
        {
            destination.ID = Guid.NewGuid();
            destination.AssetID = source.AssetID;
            destination.AssignedBayID = source.AssignedBay;
        }

        #endregion Methods
  
    }

}