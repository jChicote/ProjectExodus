using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectExodus.Domain.Entities;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace ProjectExodus.Backend.JsonDataContext
{

    public class JsonDataContext : IDataContext
    {

        #region - - - - - - Fields - - - - - -

        private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
        private static readonly string FILENAME = "GameData";
        private static readonly string FILEPATH = $"{SAVE_FOLDER}/{FILENAME}.json";

        private GameData m_GameData;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IDataContext.Add<TEntity>(TEntity newObject)
        {
            if (typeof(TEntity) == typeof(GameOptions))
                this.m_GameData.GameOptions.Add(newObject as GameOptions);
            else if (typeof(TEntity) == typeof(GameSave))
                this.m_GameData.GameSaves.Add(newObject as GameSave);
            else
                throw new NotSupportedException($"The entity type '{typeof(TEntity)}' is not supported.");
        }

        ICollection<TEntity> IDataContext.GetEntities<TEntity>()
        {
            if (this.m_GameData == null)
                throw new InvalidOperationException("GameData is not initialized.");
            
            if (typeof(TEntity) == typeof(GameOptions))
                return (ICollection<TEntity>)this.m_GameData.GameOptions;
            
            if (typeof(TEntity) == typeof(GameSave))
                return (ICollection<TEntity>)this.m_GameData.GameSaves;

            throw new NotSupportedException($"The entity type '{typeof(TEntity)}' is not supported.");
        }

        void IDataContext.Delete<TEntity>(Guid id)
        {
            if (typeof(TEntity) == typeof(GameOptions))
            {
                GameOptions _GameOptionToDelete = this.m_GameData.GameOptions
                                                    .SingleOrDefault(go => go.ID == id);
                this.m_GameData.GameOptions.Remove(_GameOptionToDelete);
            }
            else if (typeof(TEntity) == typeof(GameSave))
            {
                GameSave _GameSaveToDelete = this.m_GameData.GameSaves
                                                .SingleOrDefault(gs => gs.ID == id);
                this.m_GameData.GameSaves.Remove(_GameSaveToDelete);
            }
        }

        void IDataContext.Update<TEntity>(Guid searchID, TEntity objectToUpdate)
        {
            if (typeof(TEntity) == typeof(GameOptions))
            {
                int _Index = this.m_GameData.GameOptions
                                .Select((go, index) => new { go, index })
                                .Where(goi => goi.go.ID == searchID)
                                .Select(goi => goi.index)
                                .FirstOrDefault();
                
                this.m_GameData.GameOptions[_Index] = objectToUpdate as GameOptions;
            }
            else if (typeof(TEntity) == typeof(GameSave))
            {
                int _Index = this.m_GameData.GameSaves
                                .Select((gs, index) => new { gs, index })
                                .Where(gsi => gsi.gs.ID == searchID)
                                .Select(gsi => gsi.index)
                                .FirstOrDefault();

                this.m_GameData.GameSaves[_Index] = objectToUpdate as GameSave;
            }
            else
            {
                throw new NotSupportedException($"The entity type '{typeof(TEntity)}' is not supported.");
            }
        }

        async Task IDataContext.Load()
        {
            if (!Directory.Exists(SAVE_FOLDER))
                Directory.CreateDirectory(SAVE_FOLDER);

            // Create new filesave
            if (!File.Exists(FILEPATH))
            {
                this.m_GameData = new GameData();
                await ((IDataContext)this).SaveChanges(); 
            }

            // Try reading from the save file
            using var _Reader = new StreamReader(FILEPATH);
            try
            {
                // This creates a new GameData object.
                var _StringJson = await _Reader.ReadToEndAsync();
                this.m_GameData = JsonConvert.DeserializeObject<GameData>(_StringJson);
            }
            catch (FileNotFoundException ex)
            {
                Debug.LogError($"File not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred: {ex.Message}");
            }
            
            _Reader.Close();
        }

        async Task IDataContext.SaveChanges()
        {
            try
            {
                string _StringJson = JsonConvert.SerializeObject(this.m_GameData);
                await using var _Writer = new StreamWriter(FILEPATH);
                await _Writer.WriteAsync(_StringJson);
                _Writer.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save data: {ex.Message}");
            }
        }

        #endregion Methods
  
    }

}