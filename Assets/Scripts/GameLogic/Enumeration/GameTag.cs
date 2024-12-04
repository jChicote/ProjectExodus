namespace ProjectExodus.GameLogic.Enumeration
{

    public class GameTag : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        public static GameTag Player = new("Player", 1);
        public static GameTag Enemy = new("Enemy", 2);
        public static GameTag Interactable = new("Interactable", 3);

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