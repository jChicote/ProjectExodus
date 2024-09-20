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
            Debug.Log($"[LOG]: Loading scene {BaseScene.GetValue()} '{BaseScene.ToString()}'");
            sceneLoader.LoadScene(BaseScene);
            return Task.CompletedTask;
        });
        public static GameScenes DebugScene1 = new("DebugScene1", 1, sceneLoader =>
        {
            Debug.Log($"[LOG]: Loading scene {DebugScene1.GetValue()}  '{DebugScene1.ToString()}'");
            sceneLoader.LoadScene(DebugScene1);
            return Task.CompletedTask;
        }); // This is temporary and for debug purposes.

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GameScenes(string name, int value, Func<ISceneLoader, Task> loadSceneAction) : base(name, value)
            => this.LoadScene = loadSceneAction;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Func<ISceneLoader, Task> LoadScene { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public static implicit operator int(GameScenes gameScenes)
            => gameScenes.GetValue();

        public static implicit operator string(GameScenes gameScenes)
            => gameScenes.ToString();

        #endregion Methods

    }

}