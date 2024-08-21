using System;
using ProjectExodus.UserInterface.OptionsMenu;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class OptionsMenuScreenState : IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private readonly IOptionsMenuController m_OptionsMenuController;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public OptionsMenuScreenState(IOptionsMenuController optionsMenuController)
            => this.m_OptionsMenuController = optionsMenuController ?? 
                                                throw new ArgumentNullException(nameof(optionsMenuController));

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_OptionsMenuController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_OptionsMenuController.HideScreen();

        #endregion Methods
  
    }

}