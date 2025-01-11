using System.Collections;
using UnityEngine;

namespace ProjectExodus.GameLogic.Coroutines
{

    public interface ICoroutineManager
    {

        #region - - - - - - Methods - - - - - -

        Coroutine StartNewCoroutine(IEnumerator coroutine);

        #endregion Methods

    }

}