using System;
using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    public class WeaponSystemsInfoVariable : Variable<WeaponSystemsInfo>
    {

        #region - - - - - - Methods - - - - - -

        protected override bool ValueEquals(WeaponSystemsInfo val1, WeaponSystemsInfo val2) 
            => val1 == val2;

        #endregion Methods
  
    }
    
    [Serializable]
    public class WeaponSystemsInfoReference : VariableReference<WeaponSystemsInfoVariable, WeaponSystemsInfo>
    {

        #region - - - - - - Constructors - - - - - -

        public WeaponSystemsInfoReference(VarRefMode mode = VarRefMode.EnableConstant) 
            => SetMode(mode);

        public WeaponSystemsInfoReference(WeaponSystemsInfo defaultConstant)
        {
            useConstant = true;
            constantValue = defaultConstant;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public WeaponSystemsInfo Value
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