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

    void ISetupHandler.SetNext(ISetupHandler next)
        => this.m_NextHandler = next;

    void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
    {
        throw new System.NotImplementedException();
    }
}
