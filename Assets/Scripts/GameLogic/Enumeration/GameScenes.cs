namespace ProjectExodus.GameLogic.Enumeration
{

    public class GameScenes : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        public static GameScenes PersistenceScene = new("PersistenceScene", 0);
        public static GameScenes MainMenu = new("MainMenu", 1);
        public static GameScenes DebugScene1 = new("DebugScene1", 2); // This is temporary and for debug purposes.
        public static GameScenes DebugScene2 = new("DebugScene2", 3); // This is temporary and for debug purposes.
        
        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        private GameScenes(string name, int value) : base(name, value) { }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public static implicit operator int(GameScenes gameScenes)
            => gameScenes.GetValue();

        public static implicit operator string(GameScenes gameScenes)
            => gameScenes.ToString();

        #endregion Methods

    }

}