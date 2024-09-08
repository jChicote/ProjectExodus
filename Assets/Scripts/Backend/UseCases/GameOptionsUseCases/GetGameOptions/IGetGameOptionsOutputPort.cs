using System.Collections.Generic;
using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Backend.UseCases.GameOptionsUseCases.GetGameOptions
{

    public interface IGetGameOptionsOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentGameOptions(IEnumerable<GameOptions> gameOptions);

        #endregion Methods

    }

}