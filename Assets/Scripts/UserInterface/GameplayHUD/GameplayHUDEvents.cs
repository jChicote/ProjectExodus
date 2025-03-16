using ProjectExodus.GameLogic.Enumeration;

public class GameplayHUDEvents : SmartEnum
{

    #region - - - - - - Fields - - - - - -

    public static GameplayHUDEvents UpdateAfterburn = new("UpdateAfterburn", 0);

    #endregion Fields

    #region - - - - - - Constructors - - - - - -

    public GameplayHUDEvents(string name, int value) : base(name, value)
    {
    }

    #endregion Constructors

}
