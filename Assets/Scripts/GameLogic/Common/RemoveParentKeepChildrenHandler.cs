using UnityEngine;

namespace ProjectExodus.GameLogic.Common
{

    public class RemoveParentKeepChildrenHandler : MonoBehaviour
    {

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            foreach (Transform _Child in transform)
                _Child.SetParent(null);
            
            Object.Destroy(this.gameObject);
        }

        #endregion Unity Lifecycle Methods
  
    }

}