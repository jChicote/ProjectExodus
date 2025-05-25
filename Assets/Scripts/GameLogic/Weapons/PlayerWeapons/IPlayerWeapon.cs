using ProjectExodus.GameLogic.Weapons;

public interface IPlayerWeapon : IWeapon
{

    #region - - - - - - Properties - - - - - -

    WeaponType Type { get; }

    #endregion Properties
  
}
