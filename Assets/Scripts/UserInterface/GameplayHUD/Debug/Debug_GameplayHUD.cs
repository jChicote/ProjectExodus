using ProjectExodus.Management.UserInterfaceManager;
using Random = UnityEngine.Random;

public class Debug_GameplayHUD : IDebugCommandRegistrater
{

    #region - - - - - - Fields - - - - - -

    private int m_TestID = 0101;

    #endregion Fields
  
    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _AddWeaponIndicator = new DebugCommand(
            "ui_addweaponindicator",
            "Adds a weapon indicator to the Gameplay HUD",
            "ui_addweaponindicator",
            this.AddWeaponIndicator);
        DebugCommand _UpdateWeaponIndicator = new DebugCommand(
            "ui_updateweaponindicator",
            "Updates a weapon indicator to the Gameplay HUD",
            "ui_updateweaponindicator",
            this.UpdateWeaponIndicator);
        DebugCommand _DepleteWeaponIndicator = new DebugCommand(
            "ui_depleteweaponindicator",
            "Depletes a weapon indicator to the Gameplay HUD",
            "ui_depleteweaponindicator",
            this.DepleteWeaponIndicator);
            
        debugCommandSystem.RegisterCommand(_AddWeaponIndicator);
        debugCommandSystem.RegisterCommand(_UpdateWeaponIndicator);
        debugCommandSystem.RegisterCommand(_DepleteWeaponIndicator);
    }

    private void AddWeaponIndicator()
    {
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(GameplayHUDEvents.AddWeaponIndicator.ToString(), new WeaponInfo
        {
            ID = this.m_TestID,
        });
    }

    private void UpdateWeaponIndicator()
    {
        int _RandomAmmoCount = Random.Range(0, 50);
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(GameplayHUDEvents.UpdateWeaponIndicator.ToString(), new WeaponInfo()
        {
            ID = this.m_TestID,
            CurrentAmmo = _RandomAmmoCount,
            MaxAmmo = 50
        });
    }

    private void DepleteWeaponIndicator()
    {
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(GameplayHUDEvents.UpdateWeaponIndicator.ToString(), new WeaponInfo()
        {
            ID = this.m_TestID,
            CurrentAmmo = 0,
            MaxAmmo = 50
        });
    }

    #endregion Methods

}
