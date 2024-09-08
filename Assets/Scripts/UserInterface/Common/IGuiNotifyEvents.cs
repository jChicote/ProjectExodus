using System;

namespace ProjectExodus.UserInterface.Common
{

    public interface IGuiNotifyEvents
    {

        #region - - - - - - Methods - - - - - -

        event Action OnHideGui;
        
        event Action OnShowGui;

        #endregion Methods

    }

}