using System.Collections.Generic;
using UnityEngine;

namespace ProjectExodus.GameLogic.Common
{

    public class RemoveParentKeepChildrenHandler : MonoBehaviour
    {

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            List<Transform> _Children = new List<Transform>();
            
            foreach (Transform _Child in this.transform)
                _Children.Add(_Child);
            
            foreach (Transform _Child in _Children)
                _Child.SetParent(null);
            
            Object.Destroy(this.gameObject);
        }

        #endregion Unity Lifecycle Methods
  
    }

}