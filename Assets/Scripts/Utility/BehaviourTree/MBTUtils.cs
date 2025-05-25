using MBT;
using ProjectExodus.Utility.GameLogging;

namespace ProjectExodus.Utility.BehaviourTree
{

    public static class MBTUtils
    {

        #region - - - - - - Methods - - - - - -

        public static bool IsValid<T>(Variable<T> reference) 
            => reference != null && reference.Value != null && reference.Value != null;

        public static bool Validate<T>(Variable<T> reference, string sourceObjectName = "")
        {
            bool _IsValid = IsValid(reference);
            if(!IsValid(reference))
                GameLogger.LogError($"'{reference.name}' is not valid. ({sourceObjectName})");

            return _IsValid;
        }

        #endregion Methods
  
    }

}