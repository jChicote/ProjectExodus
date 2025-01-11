using System;
using ProjectExodus;
using ProjectExodus.GameLogic.Player.PlayerTargetingSystem;
using UnityEngine;
using Object = UnityEngine.Object;

public class Debug_DisplayTargetingSystemInfoCommand : IDebugCommandRegistrater
{

    #region - - - - - - Fields - - - - - -

    private DebugOverlayer m_DebugOverlayer;
    private IDebuggingDataProvider<PlayerTargetingSystemDebuggingData> m_PlayerTargetingSystemData;
    private IDebuggingDataProvider<TractorBeamTrackingHandlerData> m_TractorBeamTrackingHandlerData;
    
    #endregion Fields
    
    #region - - - - - - Constructors - - - - - -

    public Debug_DisplayTargetingSystemInfoCommand()
    {
        this.m_DebugOverlayer = DebugManager.Instance.DebugOverlayer
            ?? throw new ArgumentNullException(nameof(DebugManager.Instance.DebugOverlayer));
        this.m_PlayerTargetingSystemData =
            Object.FindFirstObjectByType<PlayerTargetingSystem>(FindObjectsInactive.Exclude)
                ?? throw new ArgumentNullException(nameof(PlayerTargetingSystem));
        this.m_TractorBeamTrackingHandlerData = 
            Object.FindFirstObjectByType<TractorBeamTrackingHandler>(FindObjectsInactive.Exclude)
                ?? throw new ArgumentNullException(nameof(TractorBeamTrackingHandler));
    }

    #endregion Constructors
  
    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _ShowTargetingSystemInfo = new DebugCommand(
            "show_targeting_info",
            "Displays overlayed information of the targeting system.", 
            "show_targeting_info",
            this.ShowTargetingSystemInfoCommand);
        
        debugCommandSystem.RegisterCommand(_ShowTargetingSystemInfo);
    }

    private void ShowTargetingSystemInfoCommand()
    {
        this.m_DebugOverlayer.AddEntry("Current Raycast Object ", _ =>
        {
            PlayerTargetingSystemDebuggingData _Data = this.m_PlayerTargetingSystemData.GetData();
            return _Data.PossibleTarget == null ? "None" : _Data.PossibleTarget.name;
        });
        this.m_DebugOverlayer.AddEntry("LockOn Distance Magnitude ", _ =>
        {
            TractorBeamTrackingHandlerData _Data = this.m_TractorBeamTrackingHandlerData.GetData();
            return _Data.NextTrackingTransform == null 
                ? 0
                : (_Data.NextTrackingTransform.position - 
                   _Data.PlayerTransform.transform.position).sqrMagnitude;
        });
    }

    #endregion Methods
  
}
