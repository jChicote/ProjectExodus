namespace ProjectExodus.GameLogic.Enumeration
{

    public class GameTag : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        public static GameTag Player = new GameTag("Player", 1);

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GameTag(string name, int value) : base(name, value) { }

        #endregion Constructors
        
        #region - - - - - - Methods - - - - - -

        public static implicit operator int(GameTag gameLayer)
            => gameLayer.GetValue();

        public static implicit operator string(GameTag gameLayer)
            => gameLayer.ToString();

        #endregion Methods
        
    }

}