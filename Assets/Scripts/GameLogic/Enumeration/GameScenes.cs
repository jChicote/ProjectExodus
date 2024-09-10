namespace ProjectExodus.GameLogic.Enumeration
{

    public class GameScenes : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        public static GameScenes BaseScene = new GameScenes("BaseScene", 1);
        public static GameScenes DebugScene1 = new GameScenes("DebugScene1", 2); // This is temporary and for debug purposes.

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GameScenes(string name, int value) : base(name, value)
        {
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        

        #endregion Methods
  
    }

}