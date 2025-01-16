using System;
using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    public class TransformVariable : Variable<Transform>
    {

        #region - - - - - - Methods - - - - - -

        protected override bool ValueEquals(Transform val1, Transform val2) 
            => val1 == val2;

        #endregion Methods
  
    }

    [Serializable]
    public class TransformReference : VariableReference<TransformVariable, Transform>
    {

        #region - - - - - - Constructors - - - - - -

        public TransformReference(VarRefMode mode = VarRefMode.EnableConstant) 
            => SetMode(mode);

        public TransformReference(Transform defaultConstant)
        {
            useConstant = true;
            constantValue = defaultConstant;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Transform Value
        {
            get => useConstant? constantValue : this.GetVariable().Value;
            set
            {
                if (useConstant)
                    constantValue = value;
                else
                    this.GetVariable().Value = value;
            }
        }

        #endregion Properties
  
    }

}