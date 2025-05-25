using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.GameLogic.Common.Timers;
using UnityEngine;

public class PickupCollectionHUDView : FadableElement
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private GameObject m_ContentGroup;
    [SerializeField] private List<HUDPickupIndicator> m_Indicators = new();

    [SerializeField] private float m_VisiblityLength;
    private EventTimer m_VisibilityTimer;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_VisibilityTimer = new EventTimer(this.m_VisiblityLength, Time.deltaTime, this.HideView, canRun: false);
        
        foreach(HUDPickupIndicator _PickupIndicator in this.m_Indicators)
            _PickupIndicator.Indicator.DeactivateIndicator();
    }

    private void Update()
    {
        if (this.m_IsPaused) return;
        
        this.m_VisibilityTimer.TickTimer();
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public void InitialiseView(List<PickupUserInterfaceAsset> selectedPickups)
    {
        for (int i = 0; i < this.m_Indicators.Count; i++)
        {
            HUDPickupIndicator _HUDIndicator = this.m_Indicators.ElementAt(i);

            if (i < selectedPickups.Count)
            {
                _HUDIndicator.Type = selectedPickups[i].PickupEnum;
                _HUDIndicator.Indicator.SetCount(0);
                _HUDIndicator.Indicator.SetImage(selectedPickups[i].Sprite);
                _HUDIndicator.Indicator.EnableIndicator();
            }
            else
                _HUDIndicator.Indicator.DeactivateIndicator();
        }
        
        this.ShowView();
    }
    
    public void UpdateCount(PickupEnum pickupToUpdate, int count)
    {
        this.m_Indicators
            .Single(i => i.Type == pickupToUpdate)
            .Indicator.SetCount(count);
        
        this.ShowView();
    }

    public void ShowView()
    {
        this.m_VisibilityTimer.ResetTimer();
        this.m_VisibilityTimer.EnableTimer();
        this.FadeIn();
    }

    public void HideView()
    {
        this.m_VisibilityTimer.DisableTimer();
        this.FadeOut();
    }

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
