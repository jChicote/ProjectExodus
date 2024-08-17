using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.OptionsMenu.UserInterfaceOptions
{

    public class UserInterfaceOptionScreenViews
    {

        #region - - - - - - Properties - - - - - -

        public GameObject UserInterfaceOptionsContentGroup { get; private set; }
        
        public Button UserInterfaceOptionTabButton { get; private set; }
        
        public Button HUDVisibilityButton { get; private set; }

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public UserInterfaceOptionScreenViews(
            GameObject userInterfaceOptionsContentGroup,
            Button userInterfaceOptionTabButton,
            Button hudVisibilityButton)
        {
            this.UserInterfaceOptionsContentGroup = userInterfaceOptionsContentGroup;
            this.UserInterfaceOptionTabButton = userInterfaceOptionTabButton;
            this.HUDVisibilityButton = hudVisibilityButton;
        }

        #endregion Constructors
  
    }

}