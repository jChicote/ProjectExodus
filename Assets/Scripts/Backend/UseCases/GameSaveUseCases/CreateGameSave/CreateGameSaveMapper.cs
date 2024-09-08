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

        #region - - - - - - Fields - - - - - -

        private readonly IProfileImageModelProvider m_ProfileImageModelProvider;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public CreateGameSaveMapper(
            IObjectMapperRegister objectMapperRegister, 
            IProfileImageModelProvider profileImageModelProvider)
        {
            this.m_ProfileImageModelProvider =
                profileImageModelProvider ?? throw new ArgumentNullException(nameof(profileImageModelProvider));
            
            objectMapperRegister
                .AddMappingAction<CreateGameSaveInputPort, GameSave>(MapCreateGameSaveInputPortToGameOptions);
            objectMapperRegister
                .AddMappingAction<GameSave, GameSaveModel>(MapGameSaveToGameSaveModel);
        }

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
            destination.ProfileImageID = source.ProfileImageID;
        }

        private void MapGameSaveToGameSaveModel(
            GameSave source,
            GameSaveModel destination)
        {
            destination.ID = source.ID;
            destination.CompletionProgress = source.CompletionProgress;
            destination.GameSaveName = source.GameSaveName;
            destination.GameSlotDisplayIndex = source.GameSlotDisplayIndex;
            destination.LastAccessedDate = source.LastAccessedDate;
            destination.ProfileImage = this.m_ProfileImageModelProvider.Provide(source.ProfileImageID);
        }

        #endregion Methods
  
    }

}