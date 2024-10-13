using ProjectExodus.Domain.Models;

namespace ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer
{

    public interface ICreatePlayerOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentCreatedPlayer(PlayerModel player);

        void PresentUnsuccessfulCreationOfPlayer();

        #endregion Methods

    }

}