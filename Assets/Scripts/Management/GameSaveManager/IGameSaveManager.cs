using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.Management.GameSaveManager
{

    public interface IGameSaveManager
    {

        #region - - - - - - Properties - - - - - -

        GameSaveModel GameSaveModel { get; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void InitializeGameSaveManager(IDataContext dataContext);
        
        void SaveGameSave();

        void SetGameSave(GameSaveModel gameSaveModel);

        #endregion Methods

    }

}