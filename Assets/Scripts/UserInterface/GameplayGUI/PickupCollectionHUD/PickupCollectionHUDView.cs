using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupCollectionHUDView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private GameObject m_ContentGroup;
    [SerializeField] private List<HUDPickupIndicator> m_Indicators = new();

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        foreach(HUDPickupIndicator _PickupIndicator in this.m_Indicators)
            _PickupIndicator.Indicator.DeactivateIndicator();
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public void InitialiseView(List<PickupUserInterfaceAsset> selectedPickups)
    {
        for (int i = 0; i < m_Indicators.Count; i++)
        {
            CollectableIndicator _Indicator = this.m_Indicators.ElementAt(i).Indicator;

            if (i >= selectedPickups.Count)
            {
                _Indicator.DeactivateIndicator();
                return;
            }
            
            _Indicator.SetCount(0);
            _Indicator.SetImage(selectedPickups[i].Sprite);
        }
    }
    
    public void UpdateCount(PickupEnum pickupToUpdate, int count)
        => this.m_Indicators
            .Single(i => i.Type == pickupToUpdate)
            .Indicator.SetCount(count);
        
    #endregion Methods
  
}

[Serializable]
public class HUDPickupIndicator
{

    #region - - - - - - Fields - - - - - -

    public PickupEnum Type;

    public CollectableIndicator Indicator;

    #endregion Fields

}
