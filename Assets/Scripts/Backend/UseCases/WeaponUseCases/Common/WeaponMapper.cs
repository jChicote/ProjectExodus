using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.WeaponUseCases.Common
{

    public class WeaponMapper
    {

        #region - - - - - - Constructors - - - - - -

        public WeaponMapper(IObjectMapperRegister mapperRegister) 
            => mapperRegister.AddMappingAction<Weapon, WeaponModel>(this.MapWeaponEntityToWeaponModel);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapWeaponEntityToWeaponModel(Weapon source, WeaponModel destination)
        {
            destination.ID = source.ID;
            destination.AssetID = source.AssetID;
            destination.AmmoSizeModifier = source.AmmoSizeModifier;
            destination.AssignedBayID = source.AssignedBayID;
            destination.FireRateModifier = source.FireRateModifier;
            destination.ReloadPeriodModifier = source.ReloadPeriodModifier;
        }

        #endregion Methods
  
    }

}