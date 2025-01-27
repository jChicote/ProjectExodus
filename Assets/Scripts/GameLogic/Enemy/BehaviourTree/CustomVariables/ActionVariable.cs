// using MBT;
// using Unity.Plastic.Newtonsoft.Json.Serialization;
// using UnityEngine;
//
// namespace ProjectExodus
// {
//
//     [AddComponentMenu("")]
//     public class ActionVariable : Variable<Action>
//     {
//
//         #region - - - - - - Methods - - - - - -
//
//         protected override bool ValueEquals(Action val1, Action val2)
//             => val1 == val2;
//
//         #endregion Methods
//   
//     }
//
//     public class ActionReference : VariableReference<ActionVariable, Action>
//     {
//         
//         #region - - - - - - Constructors - - - - - -
//
//         public ActionReference(VarRefMode mode = VarRefMode.EnableConstant) 
//             => SetMode(mode);
//
//         public ActionReference(Action defaultConstant)
//         {
//             useConstant = true;
//             constantValue = defaultConstant;
//         }
//
//         #endregion Constructors
//
//         #region - - - - - - Properties - - - - - -
//
//         public Action Value
//         {
//             get => useConstant? constantValue : this.GetVariable().Value;
//             set
//             {
//                 if (useConstant)
//                     constantValue = value;
//                 else
//                     this.GetVariable().Value = value;
//             }
//         }
//
//         #endregion Properties
//
//     }
//
// }