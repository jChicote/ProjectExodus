using System;
using System.Collections.Generic;
using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Backend.JsonDataContext
{

    [Serializable]
    public class GameData
    {

        #region - - - - - - Fields - - - - - -

        public List<GameOptions> GameOptions = new();

        public List<GameSave> GameSaves = new();

        public List<Player> Players = new();

        public List<Ship> Ships = new();

        public List<Weapon> Weapons = new();

        #endregion Fields

    }

}