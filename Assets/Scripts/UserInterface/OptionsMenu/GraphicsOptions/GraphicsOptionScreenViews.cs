using TMPro;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.OptionsMenu.GraphicsOptions
{

    public class GraphicsOptionScreenViews
    {

        #region - - - - - - Constructors - - - - - -

        public GraphicsOptionScreenViews(
            Button graphicsOptionTabButton,
            TMP_InputField heightInputField,
            TMP_InputField widthInputField,
            TMP_Dropdown displayDropdown)
        {
            this.GraphicsOptionTabButton = graphicsOptionTabButton;
            this.HeightInputField = heightInputField;
            this.WidthInputField = widthInputField;
            this.DisplayDropdown = displayDropdown;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public Button GraphicsOptionTabButton { get; private set; }
        
        public TMP_InputField HeightInputField { get; private set; }
        
        public TMP_InputField WidthInputField { get; private set; }
        
        public TMP_Dropdown DisplayDropdown { get; private set; }

        #endregion Properties
  
    }

}