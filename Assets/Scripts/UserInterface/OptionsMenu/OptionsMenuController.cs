using ProjectExodus.GameLogic.Dtos;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.OptionsMenu
{

    public class OptionsMenuController : MonoBehaviour, IOptionsMenuController
    {

        #region - - - - - - Fields - - - - - -

        [Header("Options Menu Views")]
        [SerializeField] private GameObject m_OptionsContentGroup;
        [SerializeField] private Button m_ApplyButton;
        [SerializeField] private Button m_ExitButton;

        [Header("Audio Options Views")]
        [SerializeField] private GameObject m_AudioOptionsContentGroup;
        [SerializeField] private Button m_AudioOptionTabButton;
        [SerializeField] private Button m_MuteButton;
        [SerializeField] private Slider m_EnvironmentFXVolumeSlider;
        [SerializeField] private Slider m_GameMusicVolumeSlider;
        [SerializeField] private Slider m_MasterVolumeSlider;
        [SerializeField] private Slider m_SoundFXVolumeSlider;
        [SerializeField] private Slider m_UIVolumeSlider;
        
        [Header("Input Options Views")]
        [SerializeField] private GameObject m_InputOptionsContentGroup;
        [SerializeField] private Button m_InputOptionTabButton;
        
        [Header("UserInterface Options Views")]
        [SerializeField] private GameObject m_UserInterfaceOptionsContentGroup;
        [SerializeField] private Button m_UserInterfaceOptionTabButton;
        [SerializeField] private Button m_HUDVisibilityButton;
        
        [Header("Graphics Options Views")]
        [SerializeField] private GameObject m_GraphicsOptionsContentGroup;
        [SerializeField] private Button m_GraphicsOptionTabButton;
        [SerializeField] private TMP_InputField m_WidthInputField;
        [SerializeField] private TMP_InputField m_HeightInputField;
        [SerializeField] private TMP_Dropdown m_DisplayDropdown;

        private IObjectMapper m_Mapper;
        private IUserInterfaceScreenStateManager m_UserInterfaceScreenStateManager;        
        
        private GameOptionsModel m_GameOptionsModel;
        private OptionsMenuViewModel m_ViewModel;
        
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
            this.m_MuteButton.onClick.AddListener(this.OnToggleMute);
            
            // User-Interface option event-bindings
            this.m_HUDVisibilityButton.onClick.AddListener(this.OnToggleHUDVisibility);
            
            // Graphics option event-bindings
            this.m_WidthInputField.onValueChanged.AddListener(this.OnDisplayWidthChanged);
            this.m_HeightInputField.onValueChanged.AddListener(this.OnDisplayHeightChanged);
            this.m_DisplayDropdown.onValueChanged.AddListener(this.OnDisplayDropdownSelection);
            
            // Options Menu view event-bindings
            this.m_ApplyButton.onClick.AddListener(this.OnApplyOptions);
            this.m_ExitButton.onClick.AddListener(this.OnExitOptions);
        }

        #endregion Unity Methods
  
        #region - - - - - - Events - - - - - -

        // -----------------------------------
        // Header Tab Events
        // -----------------------------------
        
        private void OnShowAudioOptions()
        {
            this.m_AudioOptionsContentGroup.SetActive(true);

            this.m_GraphicsOptionsContentGroup.SetActive(false);
            this.m_InputOptionsContentGroup.SetActive(false);
            this.m_UserInterfaceOptionsContentGroup.SetActive(false);
        }

        private void OnShowInputOptions()
        {
            this.m_InputOptionsContentGroup.SetActive(true);
            
            this.m_AudioOptionsContentGroup.SetActive(false);
            this.m_GraphicsOptionsContentGroup.SetActive(false);
            this.m_UserInterfaceOptionsContentGroup.SetActive(false);
        }

        private void OnShowGraphicsOptions()
        {
            this.m_GraphicsOptionsContentGroup.SetActive(true);

            this.m_AudioOptionsContentGroup.SetActive(false);
            this.m_InputOptionsContentGroup.SetActive(false);
            this.m_UserInterfaceOptionsContentGroup.SetActive(false);
        }

        private void OnShowUserInterfaceOptions()
        {
            this.m_UserInterfaceOptionsContentGroup.SetActive(true);

            this.m_AudioOptionsContentGroup.SetActive(false);
            this.m_GraphicsOptionsContentGroup.SetActive(false);
            this.m_InputOptionsContentGroup.SetActive(false);
        }
        
        // -----------------------------------
        // Audio Option Events
        // -----------------------------------

        private void OnEnvironmentVolumeChanged(float sliderValue) 
            => this.m_ViewModel.EnvironmentFXVolume = sliderValue;

        private void OnGameMusicVolumeChanged(float sliderValue) 
            => this.m_ViewModel.GameMusicVolume = sliderValue;

        private void OnMasterVolumeChanged(float sliderValue) 
            => this.m_ViewModel.MasterVolume = sliderValue;

        private void OnSoundFXVolumeChanged(float sliderValue) 
            => this.m_ViewModel.SoundFXVolume = sliderValue;

        private void OnUIVolumeChanged(float sliderValue) 
            => this.m_ViewModel.UIVolume = sliderValue;

        private void OnToggleMute()
            => this.m_ViewModel.IsMuted = !this.m_ViewModel.IsMuted;

        // -----------------------------------
        // User-Interface Option Events
        // -----------------------------------

        private void OnToggleHUDVisibility() 
            => this.m_ViewModel.IsHUDVisible = !this.m_ViewModel.IsHUDVisible;

        // -----------------------------------
        // Graphics Option Events
        // -----------------------------------

        private void OnDisplayWidthChanged(string widthValue)
        {
            if (!int.TryParse(widthValue, out int _Result))
                return;
            
            this.m_ViewModel.DisplayWidth = _Result;
        }

        private void OnDisplayHeightChanged(string heightValue)
        {
            if (!int.TryParse(heightValue, out int _Result))
                return;
            
            this.m_ViewModel.DisplayHeight = _Result;
        }

        private void OnDisplayDropdownSelection(int displaySelection) 
            => this.m_ViewModel.DisplayOption = (DisplayOption)displaySelection;
        
        // -----------------------------------
        // Confirmation Events
        // -----------------------------------
        
        private void OnExitOptions() 
            => this.m_UserInterfaceScreenStateManager.OpenPreviousScreen();

        private void OnApplyOptions()
        {
            this.m_Mapper.Map(this.m_ViewModel, this.m_GameOptionsModel);
            this.m_UserInterfaceScreenStateManager.OpenPreviousScreen();
        }

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        void IOptionsMenuController.InitialiseOptionsMenu(
            GameOptionsModel gameOptionsModel, 
            IObjectMapper mapper,
            IUserInterfaceScreenStateManager userInterfaceScreenStateManager)
        {
            this.m_GameOptionsModel = gameOptionsModel;
            this.m_Mapper = mapper;
            this.m_ViewModel = new OptionsMenuViewModel();
            this.m_UserInterfaceScreenStateManager = userInterfaceScreenStateManager;
            
            this.m_Mapper.Map(gameOptionsModel, this.m_ViewModel);
        }

        void IScreenStateController.HideScreen() 
            => this.m_OptionsContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen()
        {
            this.m_OptionsContentGroup.SetActive(true);
            
            // Set screen form values
            this.m_EnvironmentFXVolumeSlider.value = this.m_ViewModel.EnvironmentFXVolume;
            this.m_GameMusicVolumeSlider.value = this.m_ViewModel.GameMusicVolume;
            this.m_MasterVolumeSlider.value = this.m_ViewModel.MasterVolume;
            this.m_SoundFXVolumeSlider.value = this.m_ViewModel.SoundFXVolume;
            this.m_UIVolumeSlider.value = this.m_ViewModel.UIVolume;
            
            this.m_WidthInputField.text = this.m_ViewModel.DisplayWidth.ToString();
            this.m_HeightInputField.text = this.m_ViewModel.DisplayHeight.ToString();
            this.m_DisplayDropdown.value = (int)this.m_ViewModel.DisplayOption;
            
            // Reset active displayed tab to default
            this.m_AudioOptionsContentGroup.SetActive(true);
            this.m_GraphicsOptionsContentGroup.SetActive(false);
            this.m_InputOptionsContentGroup.SetActive(false);
            this.m_UserInterfaceOptionsContentGroup.SetActive(false);
        }

        #endregion Methods

    }

}