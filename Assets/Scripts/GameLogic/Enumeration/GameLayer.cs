namespace ProjectExodus.GameLogic.Enumeration
{

    public class GameLayer : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        public static GameLayer Ignore = new GameLayer("Ignore", 7);

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