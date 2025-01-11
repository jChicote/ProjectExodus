using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.OutputHandlers
{

    public class CreateGameSaveOutputResult : ICreateGameSaveOutputPort
    {

        #region - - - - - - Properties - - - - - -

        public GameSaveModel Result { get; set; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void ICreateGameSaveOutputPort.PresentCreatedGameSave(GameSaveModel gameSaveModel)
            => this.Result = gameSaveModel;

        #endregion Methods

    }

}