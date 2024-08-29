using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.GameLogic.Facades.GameSaveFacade
{

    public interface IGameSaveFacade
    {

        #region - - - - - - Methods - - - - - -

        void CreateGameSave(CreateGameSaveInputPort inputPort, ICreateGameSaveOutputPort outputPort);

        void GetGameSaves(IGetGameSavesOutputPort outputPort);

        void UpdateGameSave(UpdateGameSaveInputPort inputPort, IUpdateGameSaveOutputPort outputPort);

        #endregion Methods

    }

}