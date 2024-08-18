namespace ProjectExodus.UserInterface.OptionsMenu.InputOptions
{

    public class InputOptionViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly InputOptionScreenViews m_InputOptionScreenViews;
        private readonly OptionsMenuContentViews m_OptionsMenuContentViews;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public InputOptionViewModel(
            InputOptionScreenViews inputOptionScreenViews,
            OptionsMenuContentViews optionsMenuContentViews)
        {
            this.m_InputOptionScreenViews = inputOptionScreenViews;
            this.m_OptionsMenuContentViews = optionsMenuContentViews;
            
            this.m_InputOptionScreenViews.InputOptionTabButton.onClick.AddListener(this.OnShowInputOptions);
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -

        private void OnShowInputOptions()
        {
            this.m_OptionsMenuContentViews.InputOptionsContentGroup.SetActive(true);
            
            this.m_OptionsMenuContentViews.AudioOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentViews.GraphicsOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentViews.UserInterfaceOptionsContentGroup.SetActive(false);
        }

        #endregion Events
  
    }

}