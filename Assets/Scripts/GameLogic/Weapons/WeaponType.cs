using ProjectExodus.GameLogic.Enumeration;

public class WeaponType : SmartEnum
{

    #region - - - - - - Fields - - - - - -

    public static WeaponType Turrent = new WeaponType("Turrent", 0);

    #endregion Fields

    #region - - - - - - Constructors - - - - - -

    public WeaponType(string name, int value) : base(name, value) { }

    #endregion Constructors
  
}
