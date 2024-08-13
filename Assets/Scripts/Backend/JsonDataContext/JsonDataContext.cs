using System;
using System.Collections.Generic;
using System.IO;
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
            throw new NotImplementedException();
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
        {
            throw new NotImplementedException();
        }

        void IDataContext.Update<TEntity>(Guid ID, TEntity objectToUpdate)
        {
            throw new NotImplementedException();
        }

        async Task IDataContext.Load()
        {
            if (!Directory.Exists(SAVE_FOLDER))
                Directory.CreateDirectory(SAVE_FOLDER);
            Debug.Log("a.Verified directory");

            // Create new filesave
            if (!File.Exists(FILEPATH))
            {
                this.m_GameData = new GameData();
                await ((IDataContext)this).SaveChanges(); 
            }
            Debug.Log("b. Verified file");

            using var _Reader = new StreamReader(FILEPATH);
            try
            {
                // TODO: Below code is causing the thread to hang
                // TODO: Wrap ready in try catch statement and seperately process each JSON related exception.
                Debug.Log(FILEPATH);
                
                var _StringJson = await _Reader.ReadToEndAsync();
                JsonUtility.FromJsonOverwrite(_StringJson, this.m_GameData);
                Debug.Log("c. Completed load");
            }
            catch (FileNotFoundException ex)
            {
                // Handle file not found exception
                Console.WriteLine($"File not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other possible exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
            _Reader.Close();
        }

        async Task IDataContext.SaveChanges()
        {
            string _StringJson = JsonUtility.ToJson(this.m_GameData);
            await using var _Writer = new StreamWriter(FILEPATH);
            await _Writer.WriteAsync(_StringJson);
            _Writer.Close();
        }

        #endregion Methods
  
    }

}