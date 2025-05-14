using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

public class PickupCollectionHUDController : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private PickupCollectionHUDView m_View;
    private UserInterfaceSettings m_Settings;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        // Collect dependencies
        this.m_View = this.GetComponent<PickupCollectionHUDView>();
        this.m_Settings = UserInterfaceManager.Instance.UserInterfaceSettings;

        // Validate depenedencies
        string _SourceObjectName = this.gameObject.name;
        GameValidator.NotNull(this.m_View, nameof(m_View), sourceObjectName: _SourceObjectName);
        GameValidator.NotNull(this.m_Settings, nameof(m_Settings), sourceObjectName: _SourceObjectName);

        IUIEventCollection _EventCollection = UserInterfaceManager.Instance.EventCollectionRegistry;
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

    public void LoadPickups(List<PickupEnum> selectedPickups)
    {
        List<PickupEnum> _DeduplicatedList = selectedPickups
            .GroupBy(sp => sp.ToString())
            .Select(spg => spg.First())
            .ToList();
        
        
    }

    public void UpdatePickup(PickupEnum pickupToUpdate, int currentCount)
    {
        
    }

    #endregion Methods
  
}