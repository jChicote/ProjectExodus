using System;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Models;
using UnityEngine;

namespace ProjectExodus.Management.GameSaveManager
{

    public class GameSaveManager : MonoBehaviour, IGameSaveManager
    {

        #region - - - - - - Fields - - - - - -

        private IDataContext m_DataContext;
        private GameSaveModel m_SelectedGameSaveModel;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IGameSaveManager.InitializeGameSaveManager(IDataContext dataContext)
            => this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        #endregion Initializers

        #region - - - - - - Properties - - - - - -

        GameSaveModel IGameSaveManager.GameSaveModel 
            => this.m_SelectedGameSaveModel;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void IGameSaveManager.SaveGameSave()
            => this.m_DataContext.SaveChanges();

        void IGameSaveManager.SetGameSave(GameSaveModel gameSaveModel)
            => this.m_SelectedGameSaveModel = gameSaveModel ?? throw new ArgumentNullException(nameof(gameSaveModel));

        #endregion Methods
    }

}