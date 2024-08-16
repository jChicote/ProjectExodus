using System.Collections.Generic;

namespace ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions
{

    public interface IGetGameOptionsOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentGameOptions(IEnumerable<Entities.GameOptions> gameOptions);

        #endregion Methods

    }

}