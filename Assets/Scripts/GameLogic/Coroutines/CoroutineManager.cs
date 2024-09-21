using System.Collections;
using UnityEngine;

namespace ProjectExodus.GameLogic.Coroutines
{

    /// <summary>
    /// Manages the invocation of a Coroutine.
    /// </summary>
    /// <remarks>This is primarily used for non-Monobehavior objects that don't require the Unity lifecycle.</remarks>
    public class CoroutineManager : MonoBehaviour, ICoroutineManager
    {

        #region - - - - - - Fields - - - - - -

        private static CoroutineManager s_Instance;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Awake()
        {
            if (s_Instance == null)
                s_Instance = this;
            else
                Object.Destroy(gameObject);
        }

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Methods - - - - - -

        Coroutine ICoroutineManager.StartNewCoroutine(IEnumerator coroutine) 
            => this.StartCoroutine(coroutine);

        #endregion Methods
  
    }

}