using System;
using UnityEngine;

namespace ProjectExodus.GameLogic.Enumeration
{

    public class GameScenes : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        public static GameScenes BaseScene = new GameScenes("BaseScene", 0, () =>
        {
            Debug.Log($"[LOG]: Loading scene {BaseScene.GetValue()} '{BaseScene.ToString()}'");
        });
        public static GameScenes DebugScene1 = new GameScenes("DebugScene1", 1, () =>
        {
            Debug.Log($"[LOG]: Loading scene {DebugScene1.GetValue()}  '{DebugScene1.ToString()}'");
        }); // This is temporary and for debug purposes.

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GameScenes(string name, int value, Action action) : base(name, value)
            => this.Action = action;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Action Action { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public static implicit operator int(GameScenes gameScenes)
            => gameScenes.GetValue();

        public static implicit operator string(GameScenes gameScenes)
            => gameScenes.ToString();

        #endregion Methods

    }

}