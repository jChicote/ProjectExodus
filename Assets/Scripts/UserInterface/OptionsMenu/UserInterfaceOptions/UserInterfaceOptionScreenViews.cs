using UnityEngine.UI;

namespace ProjectExodus.UserInterface.OptionsMenu.UserInterfaceOptions
{

    public class UserInterfaceOptionScreenViews
    {

        #region - - - - - - Constructors - - - - - -

        public UserInterfaceOptionScreenViews(
            Button userInterfaceOptionTabButton,
            Button hudVisibilityButton)
        {
            this.UserInterfaceOptionTabButton = userInterfaceOptionTabButton;
            this.HUDVisibilityButton = hudVisibilityButton;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Button UserInterfaceOptionTabButton { get; private set; }
        
        public Button HUDVisibilityButton { get; private set; }

        #endregion Properties
  
    }

}