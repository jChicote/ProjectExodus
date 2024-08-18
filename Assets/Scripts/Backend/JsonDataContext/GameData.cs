using System;
using System.Collections.Generic;
using ProjectExodus.Backend.Entities;

namespace ProjectExodus.Backend.JsonDataContext
{

    [Serializable]
    public class GameData
    {

        #region - - - - - - Fields - - - - - -

        public List<GameOptions> GameOptions = new();

        #endregion Fields

    }

}