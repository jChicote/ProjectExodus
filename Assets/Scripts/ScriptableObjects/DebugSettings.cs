using UnityEngine;

namespace ScriptableObjects
{

    [CreateAssetMenu(fileName = "DebugSettings", menuName = "ScriptableObjects/DebugSettings", order = 0)]
    public class DebugSettings : ScriptableObject
    {

        #region - - - - - - Fields - - - - - -

        [Header("Input Control")]
        public GameObject DebugInputControl;

        #endregion Fields

    }

}