using System;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip
{

    public class CreateShipMapper
    {

        #region - - - - - - Constructors - - - - - -

        public CreateShipMapper(IObjectMapperRegister mapperRegister) 
            => mapperRegister.AddMappingAction<CreateShipInputPort, Ship>(this.MapCreateInputPortToShipEntity);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapCreateInputPortToShipEntity(CreateShipInputPort source, Ship destination)
        {
            destination.ID = Guid.NewGuid();
            destination.AssetID = source.AssetID;
            destination.Weapons = source.Weapons;
        }

        #endregion Methods
  
    }

}