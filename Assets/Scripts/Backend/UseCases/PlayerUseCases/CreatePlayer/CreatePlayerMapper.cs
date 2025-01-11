using System;
using System.Collections.Generic;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer
{

    public class CreatePlayerMapper
    {

        #region - - - - - - Constructors - - - - - -

        public CreatePlayerMapper(IObjectMapperRegister objectMapperRegister) 
            => objectMapperRegister
                .AddMappingAction<CreatePlayerInputPort, Player>(this.MapCreatePlayerInputPortToPlayerEntity);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapCreatePlayerInputPortToPlayerEntity(CreatePlayerInputPort source, Player destination)
        {
            destination.ID = Guid.NewGuid();
            destination.Ships = new List<Guid> { source.StartShip.ID };
        }

        #endregion Methods
  
    }

}