using ProjectExodus.Domain.Models;

namespace ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer
{

    public interface IGetPlayerOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentPlayer(PlayerModel player);

        void PresentPlayerNotFound();

        #endregion Methods

    }

}