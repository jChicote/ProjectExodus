using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;

namespace ProjectExodus.GameLogic.Facades.GameSaveFacade
{

    public interface IGameSaveFacade
    {

        #region - - - - - - Methods - - - - - -

        void GetGameSaves(IGetGameSavesOutputPort outputPort);

        #endregion Methods

    }

}