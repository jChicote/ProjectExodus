using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.PauseScreen
{

    public class PauseScreenView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;

        [Space]
        [SerializeField] private Button m_ResumeButton;
        [SerializeField] private Button m_OptionsButton;
        [SerializeField] private Button m_ExitButton;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Button ResumeButton
            => this.m_ResumeButton;

        public Button OptionsButton
            => this.m_OptionsButton;

        public Button ExitButton
            => this.m_ExitButton;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void ShowScreen()
            => this.m_ContentGroup.SetActive(true);

        public void HideScreen()
            => this.m_ContentGroup.SetActive(false);
        
        #endregion Methods

    }

}