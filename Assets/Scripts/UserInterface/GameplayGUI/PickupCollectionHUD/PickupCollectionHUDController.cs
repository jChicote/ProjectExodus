using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;

public class PickupCollectionHUDController : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private PickupCollectionHUDView m_View;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        // Collect dependencies
        this.m_View = this.GetComponent<PickupCollectionHUDView>();

        // Validate depenedencies
        string _SourceObjectName = this.gameObject.name;
        GameValidator.NotNull(this.m_View, nameof(m_View), sourceObjectName: _SourceObjectName);

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

    #endregion Methods
  
}
