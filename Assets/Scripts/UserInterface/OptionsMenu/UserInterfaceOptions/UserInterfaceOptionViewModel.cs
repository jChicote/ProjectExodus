namespace ProjectExodus.UserInterface.OptionsMenu.UserInterfaceOptions
{

    public class UserInterfaceOptionViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly OptionsMenuContentViews m_OptionsMenuContentViews;
        private readonly UserInterfaceOptionScreenViews m_UserInterfaceOptionScreenViews;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public bool IsHUDVisible { get; set; }

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public UserInterfaceOptionViewModel(
            OptionsMenuContentViews optionsMenuContentGroups,
            UserInterfaceOptionScreenViews userInterfaceOptionScreenViews)
        {
            this.m_OptionsMenuContentViews = optionsMenuContentGroups;
            this.m_UserInterfaceOptionScreenViews = userInterfaceOptionScreenViews;
            
            this.m_UserInterfaceOptionScreenViews.UserInterfaceOptionTabButton.onClick.AddListener(this.OnShowUserInterfaceOptions);
            this.m_UserInterfaceOptionScreenViews.HUDVisibilityButton.onClick.AddListener(this.OnToggleHUDVisibility);
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -
        
        private void OnShowUserInterfaceOptions()
        {
            this.m_OptionsMenuContentViews.UserInterfaceOptionsContentGroup.SetActive(true);

            this.m_OptionsMenuContentViews.AudioOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentViews.GraphicsOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentViews.InputOptionsContentGroup.SetActive(false);
        }

        private void OnToggleHUDVisibility() 
            => this.IsHUDVisible = !this.IsHUDVisible;

        #endregion Events
  
    }

}