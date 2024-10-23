using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.DebugSupport.OutputHandlers
{

    public class DebugCreateGameSaveOutputHandler : ICreateGameSaveOutputPort
    {

        #region - - - - - - Fields - - - - - -

        public GameSaveModel Result;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public void PresentCreatedGameSave(GameSaveModel gameSaveModel)
            => this.Result = gameSaveModel;

        #endregion Methods

    }

}