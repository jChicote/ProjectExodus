using ProjectExodus.Domain.Models;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave
{

    public interface ICreateGameSaveOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentCreatedGameSave(GameSaveModel gameSaveModel);

        #endregion Methods

    }

}