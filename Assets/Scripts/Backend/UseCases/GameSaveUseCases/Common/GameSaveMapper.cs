using System;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.Domain.Services;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.Common
{

    public class GameSaveMapper
    {

        #region - - - - - - Fields - - - - - -

        private readonly IProfileImageModelProvider m_ProfileImageModelProvider;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GameSaveMapper(
            IObjectMapperRegister mapperRegister, 
            IProfileImageModelProvider profileImageModelProvider)
        {
            this.m_ProfileImageModelProvider = 
                profileImageModelProvider ?? throw new ArgumentNullException(nameof(profileImageModelProvider));
            
            mapperRegister.AddMappingAction<GameSave, GameSaveModel>(this.MapGameSaveToGameSaveModel);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapGameSaveToGameSaveModel(
            GameSave source,
            GameSaveModel destination)
        {
            destination.ID = source.ID;
            destination.CompletionProgress = source.CompletionProgress;
            destination.GameSaveName = source.GameSaveName;
            destination.GameSlotDisplayIndex = source.GameSlotDisplayIndex;
            destination.LastAccessedDate = source.LastAccessedDate;
            destination.PlayerID = source.PlayerID;
            destination.ProfileImage = this.m_ProfileImageModelProvider.Provide(source.ProfileImageID);
        }

        #endregion Methods
  
    }

}