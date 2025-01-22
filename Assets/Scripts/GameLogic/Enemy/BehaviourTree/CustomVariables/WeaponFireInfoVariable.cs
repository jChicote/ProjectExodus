using System;
using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    public class WeaponFireInfoVariable : Variable<WeaponFireInfo>
    {

        #region - - - - - - Methods - - - - - -

        protected override bool ValueEquals(WeaponFireInfo val1, WeaponFireInfo val2) 
            => val1 == val2;

        #endregion Methods
  
    }
    
    [Serializable]
    public class WeaponFireReference : VariableReference<WeaponFireInfoVariable, WeaponFireInfo>
    {

        #region - - - - - - Constructors - - - - - -

        public WeaponFireReference(VarRefMode mode = VarRefMode.EnableConstant) 
            => SetMode(mode);

        public WeaponFireReference(WeaponFireInfo defaultConstant)
        {
            useConstant = true;
            constantValue = defaultConstant;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public WeaponFireInfo Value
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
    
    public class WeaponFireInfo
    {

        #region - - - - - - Properties - - - - - -

        public float FireRate { get; set; }
        
        public float FiringViewArc { get; set; }
        
        #endregion Properties
  
    }

}