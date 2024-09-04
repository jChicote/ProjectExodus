using System;

namespace ProjectExodus.UserInterface.Common
{

    public abstract class BaseViewModel
    {

        #region - - - - - - Events - - - - - -

        protected virtual event Action OnShow;

        protected virtual event Action OnHide;

        protected virtual event Action OnRefresh;

        #endregion Events
  
    }

}