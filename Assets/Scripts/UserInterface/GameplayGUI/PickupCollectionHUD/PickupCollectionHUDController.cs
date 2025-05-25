using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

public class PickupCollectionHUDController : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    // Required Dependencies
    private PickupCollectionHUDView m_View;
    private UserInterfaceSettings m_Settings;
    
    private List<PickupEnum> m_LoadedPickups;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        // Collect dependencies
        this.m_View = this.GetComponent<PickupCollectionHUDView>();
        this.m_Settings = UserInterfaceManager.Instance.UserInterfaceSettings;

        // Validate dependencies
        string _SourceObjectName = this.gameObject.name;
        GameValidator.NotNull(this.m_View, nameof(m_View), sourceObjectName: _SourceObjectName);
        GameValidator.NotNull(this.m_Settings, nameof(m_Settings), sourceObjectName: _SourceObjectName);

        IUIEventCollection _EventCollection = UserInterfaceManager.Instance.EventCollectionRegistry;
        _EventCollection.RegisterEvent(PickupCollectionHUDConstants.EmptyPickups, this.EmptyPickups);
        _EventCollection.RegisterEvent(
            PickupCollectionHUDConstants.LoadPickups, 
            selectedPickups => this.LoadPickups(selectedPickups as List<PickupEnum>));
        _EventCollection.RegisterEvent(PickupCollectionHUDConstants.UpdatePickup,
            pickupUpdate => this.UpdatePickup(pickupUpdate as PickupUpdateInfo));
        _EventCollection.RegisterEvent(PickupCollectionHUDConstants.ShowPickupHUD, this.ShowHUD);
        _EventCollection.RegisterEvent(PickupCollectionHUDConstants.HidePickupHUD, this.HideHUD);
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    private void LoadPickups(List<PickupEnum> selectedPickups)
    {
        List<PickupEnum> _DeduplicatedPickups = selectedPickups
            .GroupBy(sp => sp.ToString())
            .Select(spg => spg.First())
            .ToList();

        this.m_LoadedPickups = _DeduplicatedPickups;
        this.m_View.InitialiseView(_DeduplicatedPickups
            .Select(p => this.m_Settings.PickupAssets.First(pa => pa.PickupEnum == p))
            .ToList());
    }

    private void UpdatePickup(PickupUpdateInfo pickupUpdate) 
        => this.m_View.UpdateCount(pickupUpdate.PickupToUpdate, pickupUpdate.CurrentCount);

    private void EmptyPickups()
    {
        foreach(PickupEnum _LoadedPickup in this.m_LoadedPickups)
            this.m_View.UpdateCount(_LoadedPickup, 0);
    }

    private void ShowHUD()
        => this.m_View.ShowView();

    private void HideHUD()
        => this.m_View.HideView();

    #endregion Methods

}

public class PickupUpdateInfo
{

    #region - - - - - - Properties - - - - - -

    public PickupEnum PickupToUpdate { get; set; }

    public int CurrentCount { get; set; }

    #endregion Properties
  
}