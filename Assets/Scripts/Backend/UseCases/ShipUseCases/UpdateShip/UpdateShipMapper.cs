using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip
{

    public class UpdateShipMapper
    {
        
        #region - - - - - - Constructors - - - - - -

        public UpdateShipMapper(IObjectMapperRegister mapperRegister) 
            => mapperRegister.AddMappingAction<UpdateShipInputPort, Ship>(this.MapUpdateInputPortToShipEntity);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapUpdateInputPortToShipEntity(UpdateShipInputPort source, Ship destination)
        {
            destination.PlatingHealthModifier = source.PlatingHealthModifier;
            destination.ShieldHealthModifier = source.ShieldHealthModifier;
            destination.Weapons = source.Weapons;
        }

        #endregion Methods
        
    }

}