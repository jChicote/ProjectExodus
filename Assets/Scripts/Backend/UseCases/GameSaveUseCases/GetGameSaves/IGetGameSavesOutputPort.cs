using System.Collections.Generic;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves
{

    public interface IGetGameSavesOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentGameSaves(IEnumerable<GameSaveModel> gameSaves);

        #endregion Methods

    }

}