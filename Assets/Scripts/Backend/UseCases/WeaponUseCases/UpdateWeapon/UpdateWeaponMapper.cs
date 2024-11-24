using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.WeaponUseCases.UpdateWeapon
{

    public class UpdateWeaponMapper
    {

        #region - - - - - - Constructors - - - - - -

        public UpdateWeaponMapper(IObjectMapperRegister mapperRegister) 
            => mapperRegister.AddMappingAction<UpdateWeaponInputPort, Weapon>(this.MapUpdateWeaponInputPortToWeaponEntity);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapUpdateWeaponInputPortToWeaponEntity(UpdateWeaponInputPort source, Weapon destination)
        {
            // Note: In the future, there will be the option to upgrade the stats of a weapon.
            destination.AssignedBayID = source.AssignedBayID;
        }

        #endregion Methods
  
    }

}