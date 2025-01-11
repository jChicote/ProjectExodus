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
            else if (typeof(TEntity) == typeof(Player))
                this.m_GameData.Players.Add(newObject as Player);
            else if (typeof(TEntity) == typeof(Ship))
                this.m_GameData.Ships.Add(newObject as Ship);
            else if (typeof(TEntity) == typeof(Weapon))
                this.m_GameData.Weapons.Add(newObject as Weapon);
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
            if (typeof(TEntity) == typeof(Player))
                return (ICollection<TEntity>)this.m_GameData.Players; 
            if (typeof(TEntity) == typeof(Ship))
                return (ICollection<TEntity>)this.m_GameData.Ships;
            if (typeof(TEntity) == typeof(Weapon))
                return (ICollection<TEntity>)this.m_GameData.Weapons; 

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
            else if (typeof(TEntity) == typeof(Player))
            {
                Player _PlayerToDelete = this.m_GameData.Players
                    .SingleOrDefault(p => p.ID == id);
                this.m_GameData.Players.Remove(_PlayerToDelete);
            }
            else if (typeof(TEntity) == typeof(Ship))
            {
                Ship _ShipToDelete = this.m_GameData.Ships
                    .SingleOrDefault(s => s.ID == id);
                this.m_GameData.Ships.Remove(_ShipToDelete);
            }
            else if (typeof(TEntity) == typeof(Weapon))
            {
                Weapon _WeaponToDelete = this.m_GameData.Weapons
                    .SingleOrDefault(w => w.ID == id);
                this.m_GameData.Weapons.Remove(_WeaponToDelete);
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
                
                // TODO: Instead of outright replacing the entity, only update its values. Otherwise, handle in repository
                this.m_GameData.GameOptions[_Index] = objectToUpdate as GameOptions;
            }
            else if (typeof(TEntity) == typeof(GameSave))
            {
                int _Index = this.m_GameData.GameSaves
                                .Select((gs, index) => new { gs, index })
                                .Where(gsi => gsi.gs.ID == searchID)
                                .Select(gsi => gsi.index)
                                .FirstOrDefault();

                // TODO: Instead of outright replacing the entity, only update its values. Otherwise, handle properly in repository
                this.m_GameData.GameSaves[_Index] = objectToUpdate as GameSave;
            }
            else if (typeof(TEntity) == typeof(Player))
            {
                int _Index = this.m_GameData.Players
                    .Select((p, index) => new { p, index })
                    .Where(psi => psi.p.ID == searchID)
                    .Select(psi => psi.index)
                    .FirstOrDefault();

                // TODO: Instead of outright replacing the entity, only update its values. Otherwise, handle properly in repository
                this.m_GameData.Players[_Index] = objectToUpdate as Player;
            }
            else if (typeof(TEntity) == typeof(Ship))
            {
                int _Index = this.m_GameData.Ships
                    .Select((s, index) => new { s, index })
                    .Where(ssi => ssi.s.ID == searchID)
                    .Select(ssi => ssi.index)
                    .FirstOrDefault();

                // TODO: Instead of outright replacing the entity, only update its values. Otherwise, handle properly in repository
                this.m_GameData.Ships[_Index] = objectToUpdate as Ship;
            }
            else if (typeof(TEntity) == typeof(Weapon))
            {
                int _Index = this.m_GameData.Weapons
                    .Select((w, index) => new { w, index })
                    .Where(wsi => wsi.w.ID == searchID)
                    .Select(wsi => wsi.index)
                    .FirstOrDefault();

                // TODO: Instead of outright replacing the entity, only update its values. Otherwise, handle properly in repository
                this.m_GameData.Weapons[_Index] = objectToUpdate as Weapon;
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