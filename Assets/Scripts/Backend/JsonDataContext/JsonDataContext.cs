using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProjectExodus.Backend.Entities;
using UnityEngine;
using UnityEngine.Rendering.UI;
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
            else
                throw new NotSupportedException($"The entity type '{typeof(TEntity)}' is not supported.");
        }

        ICollection<TEntity> IDataContext.GetEntities<TEntity>()
        {
            if (this.m_GameData == null)
                throw new InvalidOperationException("GameData is not initialized.");
            
            if (typeof(TEntity) == typeof(GameOptions))
                return (ICollection<TEntity>)this.m_GameData.GameOptions;
            
            throw new NotSupportedException($"The entity type '{typeof(TEntity)}' is not supported.");
        }

        void IDataContext.Remove<TEntity>(TEntity objectToRemove) 
            => throw new NotImplementedException();

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
                this.m_GameData = JsonUtility.FromJson<GameData>(_StringJson);
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
                string _StringJson = JsonUtility.ToJson(this.m_GameData);
                await using  var _Writer = new StreamWriter(FILEPATH);
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