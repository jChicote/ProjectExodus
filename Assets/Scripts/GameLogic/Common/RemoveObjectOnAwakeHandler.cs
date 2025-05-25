using UnityEngine;

namespace ProjectExodus.GameLogic.Common
{

    public class RemoveObjectOnAwakeHandler : MonoBehaviour
    {
        
        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void OnAwake()
            => Object.Destroy(this.gameObject);

        #endregion Unity Lifecycle Methods
        
    }

}