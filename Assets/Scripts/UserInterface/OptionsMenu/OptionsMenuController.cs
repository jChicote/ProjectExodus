using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.OptionsMenu
{

    public class OptionsMenuController : MonoBehaviour, IOptionsMenuController, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;

        [Header("Audio Options")]
        [SerializeField] private GameObject m_AudioOptionsContentGroup;
        [SerializeField] private Button m_AudioOptionTabButton;
        
        [Header("Input Options")]
        [SerializeField] private GameObject m_InputOptionsContentGroup;
        [SerializeField] private Button m_InputOptionTabButton;
        
        [Header("UserInterface Options")]
        [SerializeField] private GameObject m_UserInterfaceOptionsContentGroup;
        [SerializeField] private Button m_UserInterfaceOptionTabButton;
        
        [Header("Graphics Options")]
        [SerializeField] private GameObject m_GraphicsOptionsContentGroup;
        [SerializeField] private Button m_GraphicsOptionTabButton;
        
        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_AudioOptionTabButton.onClick.AddListener(this.OnShowAudioOptions);
            this.m_InputOptionTabButton.onClick.AddListener(this.OnShowInputOptions);
            this.m_UserInterfaceOptionTabButton.onClick.AddListener(this.OnShowUserInterfaceOptions);
            this.m_GraphicsOptionTabButton.onClick.AddListener(this.OnShowGraphicsOptions);
        }

        #endregion Unity Methods
  
        #region - - - - - - Events - - - - - -

        private void OnShowAudioOptions()
        {
            this.m_AudioOptionsContentGroup.SetActive(true);
            Debug.Log("Showing Audio options");

            this.m_GraphicsOptionsContentGroup.SetActive(false);
            this.m_InputOptionsContentGroup.SetActive(false);
            this.m_UserInterfaceOptionsContentGroup.SetActive(false);
        }

        private void OnShowInputOptions()
        {
            this.m_InputOptionsContentGroup.SetActive(true);
            Debug.Log("Showing Input options");
            
            this.m_AudioOptionsContentGroup.SetActive(false);
            this.m_GraphicsOptionsContentGroup.SetActive(false);
            this.m_UserInterfaceOptionsContentGroup.SetActive(false);
        }

        private void OnShowGraphicsOptions()
        {
            this.m_GraphicsOptionsContentGroup.SetActive(true);
            Debug.Log("Showing Audio options");

            this.m_AudioOptionsContentGroup.SetActive(false);
            this.m_InputOptionsContentGroup.SetActive(false);
            // this.m_User
        }

        private void OnShowUserInterfaceOptions()
        {
            this.m_AudioOptionsContentGroup.SetActive(true);
            Debug.Log("Showing Audio options");
            
            // this.m_GraphicsOptionsContentGroup,
        }
        
        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        void IOptionsMenuController.InitialiseOptionsMenu()
        {
            
        }
        
        void IScreenStateController.HideScreen() 
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods

    }

}