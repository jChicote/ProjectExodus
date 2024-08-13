using System;

namespace ProjectExodus.Backend.UseCases.GameOptions.CreateGameOptions
{

    public interface ICreateGameOptionsOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentCreatedGameOptions(Entities.GameOptions gameOptions);

        #endregion Methods

    }

}