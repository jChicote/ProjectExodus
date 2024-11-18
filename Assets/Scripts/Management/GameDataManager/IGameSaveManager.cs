using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.PlayerActionFacades;

namespace ProjectExodus.Management.GameDataManager
{

    public interface IGameDataManager
    {

        #region - - - - - - Properties - - - - - -

        GameSaveModel GameSaveModel { get; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void InitializeGameSaveManager(IDataContext dataContext, IPlayerActionFacade playerActionFacade);
        
        void SaveGameSave();

        void SetGameSave(GameSaveModel gameSaveModel);

        #endregion Methods

    }

}