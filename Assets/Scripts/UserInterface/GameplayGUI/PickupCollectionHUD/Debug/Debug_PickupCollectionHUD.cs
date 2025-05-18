using System.Collections.Generic;
using ProjectExodus.Management.UserInterfaceManager;
using UnityEngine;

public class Debug_PickupCollectionHUD : IDebugCommandRegistrater
{

    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _LoadPickups = new DebugCommand(
            "ui_loadpickups",
            "Loads pickups indicators.",
            "ui_loadpickups",
            this.LoadSelectedPickups);
        DebugCommand _UpdatePickup = new DebugCommand(
            "ui_updatepickup",
            "Updates pickup indicator.",
            "ui_updatepickup",
            this.UpdatePickupCount);
        DebugCommand _EmptyPickups = new DebugCommand(
            "ui_emptypickups",
            "Updates pickup indicator.",
            "ui_emptypickups",
            this.EmptyPickups);
            
        debugCommandSystem.RegisterCommand(_LoadPickups);
        debugCommandSystem.RegisterCommand(_UpdatePickup);
        debugCommandSystem.RegisterCommand(_EmptyPickups);
    }

    // Currently loads only one as no additional collectable pickup variants exist ... yet
    private void LoadSelectedPickups()
    {
        List<PickupEnum> _Pickups = new() { PickupEnum.AutonomousSentry };

        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(PickupCollectionHUDConstants.LoadPickups, _Pickups);
    }

    private void EmptyPickups()
    {
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(PickupCollectionHUDConstants.EmptyPickups);
    }

    private void UpdatePickupCount()
    {
        // Note: The invocation will only work if the above pickup command has already been invoked.
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(PickupCollectionHUDConstants.UpdatePickup, new PickupUpdateInfo
        {
            CurrentCount = UnityEngine.Random.Range(1, 8),
            PickupToUpdate = PickupEnum.AutonomousSentry
        });
    }

    #endregion Methods
  
}
