using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

public class PickupCollectableSystem : MonoBehaviour, IPickupCollectionSystem
{

    #region - - - - - - Fields - - - - - -

    private IUIEventMediator m_EventMediator;
    
    private List<CurrentCollectablePickupState> m_SelectedPickups = new();

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_EventMediator = UserInterfaceManager.Instance.EventMediator;

        string _SourceObjectName = this.gameObject.name;
        GameValidator.NotNull(this.m_EventMediator, nameof(m_EventMediator), sourceObjectName: _SourceObjectName);
        
        // TODO: Replace with a selection menu passing in the details to the player's controller.
        // - As the details are attached to the player, have it so that its stored in the scene controller
        this.LoadSelectedCollectables(new List<PickupEnum> { PickupEnum.AutonomousSentry });
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    public void AddCollectable(ICollectablePickup collectablePickup)
    {
        CurrentCollectablePickupState _PickupState = this.m_SelectedPickups
            .SingleOrDefault(ps => ps.Type == collectablePickup.GetPickupType());

        if (_PickupState == null) return;
        
        _PickupState.CollectablePickups.Add(collectablePickup);
        this.m_EventMediator.Dispatch(PickupCollectionHUDConstants.UpdatePickup, new PickupUpdateInfo()
        {
            CurrentCount = _PickupState.CollectablePickups.Count,
            PickupToUpdate = _PickupState.Type
        });
    }

    public void LoadSelectedCollectables(List<PickupEnum> selectedPickupTypes)
    {
        foreach (PickupEnum _PickupEnum in selectedPickupTypes)
            this.m_SelectedPickups.Add(new CurrentCollectablePickupState
            {
                CollectablePickups = new(),
                Type = _PickupEnum
            });
        
        this.m_EventMediator.Dispatch(
            PickupCollectionHUDConstants.LoadPickups, 
            this.m_SelectedPickups.Select(p => p.Type).ToList());
    }

    #endregion Methods
  
}

public class CurrentCollectablePickupState
{

    #region - - - - - - Properties - - - - - -

    public PickupEnum Type { get; set; }

    public List<ICollectablePickup> CollectablePickups;

    #endregion Properties

}
