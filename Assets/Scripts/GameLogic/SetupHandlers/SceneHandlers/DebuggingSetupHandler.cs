using GameLogic.SetupHandlers;
using GameLogic.SetupHandlers.SceneHandlers;
using UnityEngine;

/// <summary>
/// Responsible for setting up the debugging behaviour for the game scene.
/// </summary>
/// <remarks>Should not be included in production, and is only locally used for testing purposes.</remarks>
public class DebuggingSetupHandler : MonoBehaviour, ISetupHandler
{

    #region - - - - - - Fields - - - - - -
    
    private ISetupHandler m_NextHandler;

    #endregion Fields

    #region - - - - - - Methods - - - - - -

    void ISetupHandler.SetNext(ISetupHandler next)
        => this.m_NextHandler = next;

    void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
    {
        DebugManager _DebugManager = DebugManager.Instance;
        _DebugManager.Initialize();
        
        this.m_NextHandler?.Handle(initializationContext);
    }

    #endregion Methods
  
}
