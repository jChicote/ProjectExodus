using System.Collections.Generic;

public interface IPickupCollectionSystem
{
    
    #region - - - - - - Methods - - - - - -

    void AddCollectable(ICollectablePickup collectablePickup);

    void LoadSelectedCollectables(List<PickupEnum> selectedPickupTypes);

    #endregion Methods

}
