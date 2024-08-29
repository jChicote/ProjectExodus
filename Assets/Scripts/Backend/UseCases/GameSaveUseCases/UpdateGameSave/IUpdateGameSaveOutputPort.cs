using ProjectExodus.Domain.Models;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave
{

    public interface IUpdateGameSaveOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentUpdatedGameSave(GameSaveModel gameSaveModel);

        #endregion Methods

    }

}