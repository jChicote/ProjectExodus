using ProjectExodus.UserInterface.OptionsMenu;
using UnityEngine;

namespace ProjectExodus.StateManagement.ScreenStates
{

    public class OptionsMenuScreenState : MonoBehaviour, IScreenState
    {

        #region - - - - - - Fields - - - - - -

        private IOptionsMenuController m_OptionsMenuController;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
            => this.m_OptionsMenuController = this.GetComponent<IOptionsMenuController>();

        #endregion Unity Lifecycle Methods
  
        // #region - - - - - - Constructors - - - - - -
        //
        // public OptionsMenuScreenState(IOptionsMenuController optionsMenuController)
        //     => this.m_OptionsMenuController = optionsMenuController ?? 
        //                                         throw new ArgumentNullException(nameof(optionsMenuController));
        //
        // #endregion Constructors
        //
        #region - - - - - - Methods - - - - - -

        void IScreenState.StartState() 
            => this.m_OptionsMenuController.ShowScreen();

        void IScreenState.EndState() 
            => this.m_OptionsMenuController.HideScreen();

        #endregion Methods
  
    }

}