using ProjectExodus.Backend.UseCases.GameOptionsUseCases.UpdateGameOptions;

namespace ProjectExodus.GameLogic.Facades.GameOptionsFacade
{

    public interface IGameOptionsFacade
    {
        
        #region - - - - - - Methods - - - - - -

        void CreateGameOptions();

        void GetGameOptions();

        void UpdateGameOptions(UpdateGameOptionsInputPort inputPort);

        #endregion Methods
        
    }

}