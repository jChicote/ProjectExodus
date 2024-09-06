using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.DeleteGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;

namespace ProjectExodus.GameLogic.Facades.GameSaveFacade
{

    public interface IGameSaveFacade
    {

        #region - - - - - - Methods - - - - - -

        void CreateGameSave(CreateGameSaveInputPort inputPort, ICreateGameSaveOutputPort outputPort);

        void DeleteGameSave(DeleteGameSaveInputPort inputPort, IDeleteGameSaveOutputPort outputPort);
        
        void GetGameSaves(IGetGameSavesOutputPort outputPort);

        void UpdateGameSave(UpdateGameSaveInputPort inputPort, IUpdateGameSaveOutputPort outputPort);

        #endregion Methods

    }

}