using UnityEngine;

public class DebugHandler : MonoBehaviour, IDebugHandler
{

    #region - - - - - - Methods - - - - - -

    void IDebugHandler.SubmitDebugCommand()
    {
        if (DebugManager.Instance == null)
            return;
        
        DebugManager.Instance.SubmitPressed();
    }

    void IDebugHandler.ToggleDebugMenu()
    {
        if (DebugManager.Instance == null)
            return;
        
        DebugManager.Instance.ShowConsolePressed();
    }

    #endregion Methods
  
}
