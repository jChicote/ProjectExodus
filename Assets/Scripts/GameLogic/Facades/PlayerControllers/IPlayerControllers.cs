using ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer;

namespace ProjectExodus.GameLogic.Facades.PlayerControllers
{

    public interface IPlayerControllers
    {

        #region - - - - - - Methods - - - - - -

        void CreatePlayer(CreatePlayerInputPort inputPort, ICreatePlayerOutputPort outputPort);

        void GetPlayer(GetPlayerInputPort inputPort, IGetPlayerOutputPort outputPort);

        void UpdatePlayer(UpdatePlayerInputPort inputPort, IUpdatePlayerOutputPort outputPort);

        #endregion Methods

    }

}