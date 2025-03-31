using ProjectExodus.GameLogic.Enumeration;

public class WeaponType : SmartEnum
{

    #region - - - - - - Fields - - - - - -

    public static WeaponType Turrent = new WeaponType("Turrent", 0);

    #endregion Fields

    #region - - - - - - Constructors - - - - - -

    public WeaponType(string name, int value) : base(name, value) { }

    #endregion Constructors

    #region - - - - - - Methods - - - - - -

    public WeaponType ConvertFromEnum(WeaponTypeEnum weaponType)
    {
        if (weaponType.ToString() == Turrent.ToString())
            return Turrent;

        return Turrent;
    }

    #endregion Methods
  
}

/// <summary>
/// Enum intended for use in inspector
/// </summary>
public enum WeaponTypeEnum
{
    Turrent
}
