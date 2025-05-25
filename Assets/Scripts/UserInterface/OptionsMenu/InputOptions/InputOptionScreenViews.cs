using UnityEngine.UI;

namespace ProjectExodus.UserInterface.OptionsMenu.InputOptions
{

    public class InputOptionScreenViews
    {

        #region - - - - - - Constructors - - - - - -

        public InputOptionScreenViews(Button inputOptionTabButton) 
            => this.InputOptionTabButton = inputOptionTabButton;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Button InputOptionTabButton { get; private set; }

        #endregion Properties
  
    }

}