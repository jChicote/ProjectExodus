using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer
{

    public class UpdatePlayerMapper
    {

        #region - - - - - - Constructors - - - - - -

        public UpdatePlayerMapper(IObjectMapperRegister mapperRegister)
            => mapperRegister
                .AddMappingAction<UpdatePlayerInputPort, Player>(this.MapUpdatePlayerInputPortToPlayerEntity);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapUpdatePlayerInputPortToPlayerEntity(UpdatePlayerInputPort source, Player destination) 
            => destination.Ships = source.Ships;

        #endregion Methods
  
    }

}