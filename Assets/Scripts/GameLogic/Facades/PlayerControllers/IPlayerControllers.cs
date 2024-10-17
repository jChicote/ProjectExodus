using ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;

namespace ProjectExodus.GameLogic.Facades.PlayerControllers
{

    public interface IPlayerControllers
    {

        #region - - - - - - Methods - - - - - -

        void CreatePlayer(CreatePlayerInputPort inputPort, ICreatePlayerOutputPort outputPort);

        void GetPlayer(GetPlayerInputPort inputPort, IGetPlayerOutputPort outputPort);

        #endregion Methods

    }

}