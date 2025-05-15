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
            pickupUpdate => this.UpdatePickup(pickupUpdate as PickupUpdateRequest));
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    /*
     * Decided actions for interface:
     * - Add pickup to set collection sorted by type
     * - Remove pickup when toggled from the player
     * - Clear pickups on death
     * - Hide pickup when not in use
     * - Show pickup when used or when forced to reveal
     */

    private void LoadPickups(List<PickupEnum> selectedPickups)
    {
        List<PickupEnum> _DeduplicatedList = selectedPickups
            .GroupBy(sp => sp.ToString())
            .Select(spg => spg.First())
            .ToList();
        
        this.m_View.InitialiseView(_DeduplicatedList
            .Select(p => this.m_Settings.PickupAssets.First(pa => pa.PickupEnum == p))
            .ToList());
    }

    private void UpdatePickup(PickupUpdateRequest pickupUpdate) 
        => this.m_View.UpdateCount(pickupUpdate.PickupToUpdate, pickupUpdate.CurrentCount);

    private void EmptyPickups()
    {
        foreach(PickupEnum _LoadedPickup in this.m_LoadedPickups)
            this.m_View.UpdateCount(_LoadedPickup, 0);
    }
    
    #endregion Methods
  
}

public class PickupUpdateRequest
{

    #region - - - - - - Properties - - - - - -

    public PickupEnum PickupToUpdate { get; set; }

    public int CurrentCount { get; set; }

    #endregion Properties
  
}