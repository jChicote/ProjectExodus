using ProjectExodus.GameLogic.Models;
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
        [SerializeField] private Button m_MuteButton;
        [SerializeField] private Slider m_EnvironmentFXVolumeSlider;
        [SerializeField] private Slider m_GameMusicVolumeSlider;
        [SerializeField] private Slider m_MasterVolumeSlider;
        [SerializeField] private Slider m_SoundFXVolumeSlider;
        [SerializeField] private Slider m_UIVolumeSlider;
        
        [Header("Input Options")]
        [SerializeField] private GameObject m_InputOptionsContentGroup;
        [SerializeField] private Button m_InputOptionTabButton;
        
        [Header("UserInterface Options")]
        [SerializeField] private GameObject m_UserInterfaceOptionsContentGroup;
        [SerializeField] private Button m_UserInterfaceOptionTabButton;
        [SerializeField] private Button m_HUDVisibilityButton;
        
        [Header("Graphics Options")]
        [SerializeField] private GameObject m_GraphicsOptionsContentGroup;
        [SerializeField] private Button m_GraphicsOptionTabButton;
        [SerializeField] private InputField m_WidthInputField;
        [SerializeField] private InputField m_HeightInputField;
        [SerializeField] private Dropdown m_DisplayDropdown;

        private GameOptions m_GameOptions;
        
        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            // Header tab event-bindings
            this.m_AudioOptionTabButton.onClick.AddListener(this.OnShowAudioOptions);
            this.m_InputOptionTabButton.onClick.AddListener(this.OnShowInputOptions);
            this.m_UserInterfaceOptionTabButton.onClick.AddListener(this.OnShowUserInterfaceOptions);
            this.m_GraphicsOptionTabButton.onClick.AddListener(this.OnShowGraphicsOptions);
            
            // Audio option event-bindings
            this.m_EnvironmentFXVolumeSlider.onValueChanged.AddListener(this.OnEnvironmentVolumeChanged);
            this.m_GameMusicVolumeSlider.onValueChanged.AddListener(this.OnGameMusicVolumeChanged);
            this.m_MasterVolumeSlider.onValueChanged.AddListener(this.OnMasterVolumeChanged);
            this.m_SoundFXVolumeSlider.onValueChanged.AddListener(this.OnSoundFXVolumeChanged);
            this.m_UIVolumeSlider.onValueChanged.AddListener(this.OnUIVolumeChanged);
            
            // User-Interface option event-bindings
            this.m_HUDVisibilityButton.onClick.AddListener(this.OnToggleHUDVisibility);
            
            // Graphics option event-bindings
            this.m_WidthInputField.onSubmit.AddListener(this.OnDisplayWidthChanged);
            this.m_HeightInputField.onSubmit.AddListener(this.OnDisplayHeightChanged);
            this.m_DisplayDropdown.onValueChanged.AddListener(this.OnDisplayDropdownSelection);
        }

        #endregion Unity Methods
  
        #region - - - - - - Events - - - - - -

        // -----------------------------------
        // Header Tab Events
        // -----------------------------------
        
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
            this.m_UserInterfaceOptionsContentGroup.SetActive(false);
        }

        private void OnShowUserInterfaceOptions()
        {
            this.m_UserInterfaceOptionsContentGroup.SetActive(true);
            Debug.Log("Showing User-Interface options");

            this.m_AudioOptionsContentGroup.SetActive(false);
            this.m_GraphicsOptionsContentGroup.SetActive(false);
            this.m_InputOptionsContentGroup.SetActive(false);
        }
        
        // -----------------------------------
        // Audio Option Events
        // -----------------------------------

        private void OnEnvironmentVolumeChanged(float sliderValue)
        {
            
        }

        private void OnGameMusicVolumeChanged(float sliderValue)
        {
            
        }

        private void OnMasterVolumeChanged(float sliderValue)
        {
            
        }

        private void OnSoundFXVolumeChanged(float sliderValue)
        {
            
        }

        private void OnUIVolumeChanged(float sliderValue)
        {
            
        }
        
        // -----------------------------------
        // User-Interface Option Events
        // -----------------------------------

        private void OnToggleHUDVisibility()
        {
            
        }
        
        // -----------------------------------
        // Graphics Option Events
        // -----------------------------------

        private void OnDisplayWidthChanged(string widthValue)
        {
            
        }

        private void OnDisplayHeightChanged(string heightValue)
        {
            
        }

        private void OnDisplayDropdownSelection(int displaySelection)
        {
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