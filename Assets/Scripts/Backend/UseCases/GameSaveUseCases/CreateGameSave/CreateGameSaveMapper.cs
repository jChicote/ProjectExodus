using System;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.CreateGameOptions;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.Domain.Services;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave
{

    public class CreateGameSaveMapper
    {

        #region - - - - - - Constructors - - - - - -

        public CreateGameSaveMapper(
            IObjectMapperRegister objectMapperRegister) 
            => objectMapperRegister
                .AddMappingAction<CreateGameSaveInputPort, GameSave>(MapCreateGameSaveInputPortToGameOptions);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapCreateGameSaveInputPortToGameOptions(
            CreateGameSaveInputPort source,
            GameSave destination)
        {
            if (source == null || destination == null)
                throw new ArgumentNullException($"Mapping objects of type: {typeof(CreateGameOptionsInputPort)}" +
                                                $" and {typeof(GameOptions)}, cannot both or either be null.");

            destination.ID = Guid.NewGuid();
            destination.CompletionProgress = source.CompletionProgress;
            destination.GameSaveName = source.GameSaveName;
            destination.GameSlotDisplayIndex = source.GameSlotDisplayIndex;
            destination.LastAccessedDate = source.LastAccessedDate;
            destination.PlayerID = source.PlayerID;
            destination.ProfileImageID = source.ProfileImageID;
        }

        #endregion Methods
  
    }

}