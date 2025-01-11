using ProjectExodus.Utility.GameLogging;

namespace ProjectExodus.Utility.GameValidation
{

    public static class GameValidator
    {

        #region - - - - - - Methods - - - - - -

        public static bool NotNull(object objectToCheck, string sourceName, bool canShowMessage = true)
        {
            if (objectToCheck != null) return true;

            if (canShowMessage)
                GameLogger.LogError($"'{sourceName}' is found to be null. Please set a value.");

            return false;
        }

        #endregion Methods
  
    }

}