using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace ProjectExodus.Backend.JsonDataContext
{

    public class JsonDataContext : IDataContext
    {

        #region - - - - - - Fields - - - - - -

        private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
        private static readonly string FILENAME = "GameData";

        private readonly Dictionary<Type, ICollection<object>> m_Entities = new();

        private GameData m_GameData;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public JsonDataContext()
        {
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IDataContext.Add<TEntity>(TEntity newObject)
        {
            throw new NotImplementedException();
        }

        ICollection<TEntity> IDataContext.GetEntities<TEntity>()
        {
            throw new NotImplementedException();
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
            
            if (!File.Exists($"{SAVE_FOLDER}/{FILENAME}.json"))
                await ((IDataContext)this).SaveChanges(); // Should create new file

            using var _Reader = new StreamReader($"{SAVE_FOLDER}/{FILENAME}.json");
            var _StringJson = await _Reader.ReadToEndAsync();
            JsonUtility.FromJsonOverwrite(_StringJson, this.m_GameData);
            _Reader.Close();
        }

        async Task IDataContext.SaveChanges()
        {
            string _StringJson = JsonUtility.ToJson(this.m_GameData);
            await using var _Writer = new StreamWriter($"{SAVE_FOLDER}/{FILENAME}.json");
            await _Writer.WriteAsync(_StringJson);
            _Writer.Close();
        }

        #endregion Methods
  
    }

}