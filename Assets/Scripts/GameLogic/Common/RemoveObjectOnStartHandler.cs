using UnityEngine;

namespace ProjectExodus.GameLogic.Common
{

    public class RemoveObjectOnStartHandler : MonoBehaviour
    {

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void OnEnable()
            => Object.Destroy(this.gameObject);

        #endregion Unity Lifecycle Methods

    }

}