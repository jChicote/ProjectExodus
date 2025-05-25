namespace ProjectExodus.GameLogic.Enumeration
{

    public class GameLayer : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        public static GameLayer Ignore = new("Ignore", 6);
        public static GameLayer Raycast = new("Raycast", 7);
        public static GameLayer Environment = new("Environment", 8);
        public static GameLayer Enemies = new("Enemies", 9);
        public static GameLayer Player = new GameLayer("Player", 10);

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameLayer(string name, int value) : base(name, value) { }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public static implicit operator int(GameLayer gameLayer)
            => gameLayer.GetValue();

        public static implicit operator string(GameLayer gameLayer)
            => gameLayer.ToString();

        #endregion Methods
  
    }

}