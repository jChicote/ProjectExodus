using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;

namespace ProjectExodus.GameLogic.Facades.GameSaveFacade
{

    public interface IGameSaveFacade
    {

        #region - - - - - - Methods - - - - - -

        void CreateGameSave(ICreateGameSaveOutputPort outputPort);

        void GetGameSaves(IGetGameSavesOutputPort outputPort);

        void UpdateGameSave(IUpdateGameSaveOutputPort outputPort);

        #endregion Methods

    }

}