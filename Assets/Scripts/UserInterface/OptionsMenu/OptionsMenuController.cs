using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.UseCases.GameOptions.UpdateGameOptions;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using ProjectExodus.UserInterface.OptionsMenu.AudioOptions;
using ProjectExodus.UserInterface.OptionsMenu.GraphicsOptions;
using ProjectExodus.UserInterface.OptionsMenu.InputOptions;
using ProjectExodus.UserInterface.OptionsMenu.UserInterfaceOptions;
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

        private IDataContext m_DataContext;
        private IGameOptionsFacade m_GameOptionsFacade;
        private IObjectMapper m_Mapper;
        private IUserInterfaceScreenStateManager m_UserInterfaceScreenStateManager;        
        
        private GameOptionsModel m_GameOptionsModel;
        private AudioOptionViewModel m_AudioOptionViewModel;
        private InputOptionViewModel m_InputOptionViewModel;
        private GraphicsOptionViewModel m_GraphicsOptionViewModel;
        private UserInterfaceOptionViewModel m_UserInterfaceOptionViewModel;
        
        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            // Options Menu view event-bindings
            this.m_ApplyButton.onClick.AddListener(this.OnApplyOptions);
            this.m_ExitButton.onClick.AddListener(this.OnExitOptions);
        }

        #endregion Unity Methods

        #region - - - - - - Initializers - - - - - -

        void IOptionsMenuController.InitialiseOptionsMenu(
            IDataContext dataContext,
            GameOptionsModel gameOptionsModel, 
            IGameOptionsFacade gameOptionsFacade,
            IObjectMapper mapper,
            IUserInterfaceScreenStateManager userInterfaceScreenStateManager)
        {
            // Set model reference
            this.m_GameOptionsModel = gameOptionsModel;

            // Initialize Services
            this.m_DataContext = dataContext; // Ticket #43 - Change this to use the Save Manager contract
            this.m_GameOptionsFacade = gameOptionsFacade;
            this.m_Mapper = mapper;
            this.m_UserInterfaceScreenStateManager = userInterfaceScreenStateManager;

            // Initialise View Models
            OptionsMenuContentViews _OptionsMenuContentViews = new OptionsMenuContentViews
            (
                this.m_AudioOptionsContentGroup,
                this.m_InputOptionsContentGroup,
                this.m_GraphicsOptionsContentGroup,
                this.m_UserInterfaceOptionsContentGroup
            );

            this.m_AudioOptionViewModel = new AudioOptionViewModel
            (
                new AudioOptionScreenViews
                (
                    this.m_AudioOptionTabButton,
                    this.m_MuteButton,
                    this.m_EnvironmentFXVolumeSlider,
                    this.m_GameMusicVolumeSlider,
                    this.m_MasterVolumeSlider,
                    this.m_SoundFXVolumeSlider,
                    this.m_UIVolumeSlider
                ),
                _OptionsMenuContentViews
            );

            this.m_InputOptionViewModel = new InputOptionViewModel
            (
                new InputOptionScreenViews(this.m_InputOptionTabButton),
                _OptionsMenuContentViews
            );

            this.m_GraphicsOptionViewModel = new GraphicsOptionViewModel
            (
                _OptionsMenuContentViews,
                new GraphicsOptionScreenViews
                (
                    this.m_GraphicsOptionTabButton,
                    this.m_HeightInputField,
                    this.m_WidthInputField,
                    this.m_DisplayDropdown
                )
            );

            this.m_UserInterfaceOptionViewModel = new UserInterfaceOptionViewModel
            (
                _OptionsMenuContentViews,
                new UserInterfaceOptionScreenViews
                (
                    this.m_UserInterfaceOptionTabButton,
                    this.m_HUDVisibilityButton
                )
            );
            
            // Map starting values to View Models
            this.m_Mapper.Map(this.m_GameOptionsModel, this.m_AudioOptionViewModel);
            this.m_Mapper.Map(this.m_GameOptionsModel, this.m_GraphicsOptionViewModel);
            this.m_Mapper.Map(this.m_GameOptionsModel, this.m_UserInterfaceOptionViewModel);
        }

        #endregion Initializers
          
        #region - - - - - - Events - - - - - -

        private void OnExitOptions() 
            => this.m_UserInterfaceScreenStateManager.OpenPreviousScreen();

        private void OnApplyOptions()
        {
            UpdateGameOptionsInputPort _InputPort = new UpdateGameOptionsInputPort { ID = m_GameOptionsModel.ID };
            this.m_Mapper.Map(this.m_AudioOptionViewModel, _InputPort);
            this.m_Mapper.Map(this.m_GraphicsOptionViewModel, _InputPort);
            this.m_Mapper.Map(this.m_UserInterfaceOptionViewModel, _InputPort);
            this.m_GameOptionsFacade.UpdateGameOptions(_InputPort);
            this.m_DataContext.SaveChanges(); // Ticket #43 - Change this to use the Save Manager contract
            
            this.m_UserInterfaceScreenStateManager.OpenPreviousScreen();
        }

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen() 
            => this.m_OptionsContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen()
        {
            this.m_OptionsContentGroup.SetActive(true);
            
            // Update ViewModel values
            this.m_Mapper.Map(this.m_GameOptionsModel, this.m_AudioOptionViewModel);
            this.m_Mapper.Map(this.m_GameOptionsModel, this.m_GraphicsOptionViewModel);
            this.m_Mapper.Map(this.m_GameOptionsModel, this.m_UserInterfaceOptionViewModel);

            // Reset active displayed tab to default
            this.m_AudioOptionsContentGroup.SetActive(true);
            this.m_GraphicsOptionsContentGroup.SetActive(false);
            this.m_InputOptionsContentGroup.SetActive(false);
            this.m_UserInterfaceOptionsContentGroup.SetActive(false);
        }

        #endregion Methods

    }
    
}