public interface IWeaponSystem
{

    #region - - - - - - Methods - - - - - -

    void EnableWeaponFire();
        
    void DisableWeaponFire();

    #endregion Methods

}

public class SentryDroneWeaponSystems : IWeaponSystem
{
    public void EnableWeaponFire()
    {
        throw new System.NotImplementedException();
    }

    public void DisableWeaponFire()
    {
        throw new System.NotImplementedException();
    }
}
