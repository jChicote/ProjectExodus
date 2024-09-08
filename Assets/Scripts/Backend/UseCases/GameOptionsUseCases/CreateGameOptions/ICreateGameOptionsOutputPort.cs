using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Backend.UseCases.GameOptionsUseCases.CreateGameOptions
{

    public interface ICreateGameOptionsOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentCreatedGameOptions(GameOptions gameOptions);

        #endregion Methods

    }

}