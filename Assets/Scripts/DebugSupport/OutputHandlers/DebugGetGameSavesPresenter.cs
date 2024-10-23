using System.Collections.Generic;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.DebugSupport.OutputHandlers
{

    public class DebugGetGameSavesPresenter : IGetGameSavesOutputPort
    {

        #region - - - - - - Fields - - - - - -

        public IEnumerable<GameSaveModel> Result;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public void PresentGameSaves(IEnumerable<GameSaveModel> gameSaves) 
            => Result = gameSaves;

        #endregion Methods
  
    }

}