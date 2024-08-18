using ProjectExodus.GameLogic.Enumeration;

namespace ProjectExodus.UserInterface.OptionsMenu.GraphicsOptions
{

    public class GraphicsOptionViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly GraphicsOptionScreenViews m_GraphicsOptionsScreenViews;
        private readonly OptionsMenuContentViews m_OptionsMenuContentViews;

        private DisplayOption m_DisplayOption;
        private int m_DisplayHeight;
        private int m_DisplayWidth;
        
        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GraphicsOptionViewModel(
            OptionsMenuContentViews optionsMenuContentViews,
            GraphicsOptionScreenViews graphicsOptionScreenViews)
        {
            this.m_GraphicsOptionsScreenViews = graphicsOptionScreenViews;
            this.m_OptionsMenuContentViews = optionsMenuContentViews;
            
            this.m_GraphicsOptionsScreenViews.GraphicsOptionTabButton.onClick.AddListener(this.OnShowGraphicsOptions);
            
            this.m_GraphicsOptionsScreenViews.WidthInputField.onValueChanged.AddListener(this.OnDisplayWidthChanged);
            this.m_GraphicsOptionsScreenViews.HeightInputField.onValueChanged.AddListener(this.OnDisplayHeightChanged);
            this.m_GraphicsOptionsScreenViews.DisplayDropdown.onValueChanged.AddListener(this.OnDisplayDropdownSelection);
        }

        #endregion Constructors
        
        #region - - - - - - Properties - - - - - -

        public DisplayOption DisplayOption
        {
            get => this.m_DisplayOption;
            set
            {
                if (this.m_DisplayOption == value) return;
                this.m_DisplayOption = value;
                this.m_GraphicsOptionsScreenViews.DisplayDropdown.value = (int)value;
            }
        }

        public int DisplayHeight
        {
            get => this.m_DisplayHeight;
            set
            {
                if (this.m_DisplayHeight == value) return;
                this.m_DisplayHeight = value;
                this.m_GraphicsOptionsScreenViews.HeightInputField.text = value.ToString();
            }
        }

        public int DisplayWidth
        {
            get => this.m_DisplayWidth;
            set
            {
                if (this.m_DisplayWidth == value) return;
                this.m_DisplayWidth = value;
                this.m_GraphicsOptionsScreenViews.WidthInputField.text = value.ToString();
            }
        }

        #endregion Properties

        #region - - - - - - Events - - - - - -

        private void OnDisplayWidthChanged(string widthValue)
        {
            if (!int.TryParse(widthValue, out int _Result))
                return;
            
            this.DisplayWidth = _Result;
        }

        private void OnDisplayHeightChanged(string heightValue)
        {
            if (!int.TryParse(heightValue, out int _Result))
                return;
            
            this.DisplayHeight = _Result;
        }

        private void OnDisplayDropdownSelection(int displaySelection) 
            => this.DisplayOption = (DisplayOption)displaySelection;
        
        private void OnShowGraphicsOptions()
        {
            this.m_OptionsMenuContentViews.GraphicsOptionsContentGroup.SetActive(true);

            this.m_OptionsMenuContentViews.AudioOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentViews.InputOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentViews.UserInterfaceOptionsContentGroup.SetActive(false);
        }

        #endregion Events
  
    }

}