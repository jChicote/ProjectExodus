using UnityEngine;
using UnityEngine.Events;

namespace ProjectExodus
{

    public interface IPlayerObserver
    {


        #region - - - - - - Properties - - - - - -

        UnityEvent<GameObject> OnPlayerSpawned { get; }
        
        UnityEvent OnPlayerDeath { get; }
        
        #endregion Properties

    }

}