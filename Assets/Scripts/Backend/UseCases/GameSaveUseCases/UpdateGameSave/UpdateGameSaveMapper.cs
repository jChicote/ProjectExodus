using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave
{

    public class UpdateGameSaveMapper
    {

        #region - - - - - - Constructors - - - - - -

        public UpdateGameSaveMapper(IObjectMapperRegister objectMapperRegister) 
            => objectMapperRegister
                .AddMappingAction<UpdateGameSaveInputPort, GameSave>(MapUpdateGameSaveInputPortToGameSave);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapUpdateGameSaveInputPortToGameSave(
            UpdateGameSaveInputPort source, 
            GameSave destination)
        {
            destination.ID = source.ID;
            destination.CompletionProgress = source.CompletionProgress;
            destination.GameSaveName = source.GameSaveName;
            destination.ProfileImageID = source.SelectedProfileImageID;
        }

        #endregion Methods
  
    }

}