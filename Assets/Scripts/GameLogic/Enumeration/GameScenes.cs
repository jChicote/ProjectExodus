using System;
using System.Threading.Tasks;
using ProjectExodus.GameLogic.Scene.SceneLoader;
using UnityEngine;

namespace ProjectExodus.GameLogic.Enumeration
{

    public class GameScenes : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        public static GameScenes BaseScene = new("BaseScene", 0, sceneLoader =>
        {
            Debug.Log($"[LOG]: Loading scene {BaseScene.GetValue()} -> '{BaseScene}'");
            sceneLoader.LoadScene(BaseScene);
        });
        public static GameScenes DebugScene1 = new("DebugScene1", 1, sceneLoader =>
        {
            sceneLoader.LoadScene(DebugScene1);
            Debug.Log($"[LOG]: Loaded scene {DebugScene1.GetValue()} -> '{DebugScene1}'");
        }); // This is temporary and for debug purposes.

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        private GameScenes(string name, int value, Action<ISceneLoader> loadSceneAction) : base(name, value)
            => this.LoadScene = loadSceneAction;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Action<ISceneLoader> LoadScene { get; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public static implicit operator int(GameScenes gameScenes)
            => gameScenes.GetValue();

        public static implicit operator string(GameScenes gameScenes)
            => gameScenes.ToString();

        #endregion Methods

    }

}