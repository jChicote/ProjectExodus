using System.Collections.Generic;
using System.Linq;
using ProjectExodus;
using ProjectExodus.GameLogic.Input;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugManager : MonoBehaviour, IDebugCommandSystem
{

    #region - - - - - - Fields - - - - - -

    public static DebugManager Instance;

    public List<object> CommandList = new();
    public DebugOverlayer DebugOverlayer;
    
    [SerializeField] private DebugConsole m_Console;
    [SerializeField] private DebugSettings m_DebugSettings;
    
    private GameObject m_DebugInputController;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion Unity Methods

    #region - - - - - - Initializers - - - - - -

    // Custom initializer is used to ensure that added commands have valid function references after the scene starts.
    public void Initialize()
    {
        this.InitializeCommands();

        IPlayerObserver _PlayerObserver = SceneManager.Instance.SceneController.PlayerObserver;
        _PlayerObserver.OnPlayerDeath.AddListener(this.CreateTemporaryDebugInputHandler);
        _PlayerObserver.OnPlayerSpawned.AddListener(_ => this.RemoveTemporaryDebugInputHandler());
    }

    #endregion Initializers
  
    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(object command)
        => this.CommandList.Add(command);

    public void UnregisterCommand(string id)
    {
        if (this.CommandList.Any(c => c is DebugCommandBase _BaseCommand && _BaseCommand.CommandID == id))
            return;
        
        var _Command = this.CommandList.First(c => c is DebugCommandBase _BaseCommand && _BaseCommand.CommandID == id);
        this.CommandList.Remove(_Command);
    }

    public void ShowConsolePressed()
        => this.m_Console.OnConsolePressed();

    public void SubmitPressed()
        => this.m_Console.OnSubmit();

    private void InitializeCommands()
    {
        this.CommandList.Clear();
        this.CommandList.Add(new DebugCommand("help", "Shows all commands", "help", () => this.m_Console.ShowHelp(true)));
        
        // TODO: Change the below command to instead invoke the SceneControl implementation of scene switching.
        // this.CommandList.Add(new DebugCommand("reset", "Restarts the game", "reset", () => SceneManager.LoadScene(GameScenes.MainMenu)));

        // Configure command for clearing overlay entries
        this.CommandList.Add(new DebugCommand(
            "clear_overlay", 
            "Clears the overlay of debugging info", 
            "clear_overlay",
            () => this.DebugOverlayer.ClearEntries()));
        
        // Register all commands
        DebugCommandConfigurator _CommandConfigurator = new DebugCommandConfigurator();
        _CommandConfigurator.ConfigureCommands();
    }

    // As the InputControl exists on the player and can be destroyed.
    // A separate input control is created to preserve debug console interactions.
    private void CreateTemporaryDebugInputHandler()
    {
        this.m_DebugInputController = Instantiate(this.m_DebugSettings.DebugInputControl, this.transform);
        
        IInitialize<DebugInputControlInitializerData> _Initializer =
            this.m_DebugInputController.GetComponent<IInitialize<DebugInputControlInitializerData>>();
        _Initializer.Initialize(new() { DebugHandler = this.GetComponent<IDebugHandler>() });
        IInputControl _InputControl = this.m_DebugInputController.GetComponent<IInputControl>();
        
        PlayerInput _PlayerInput = InputManager.Instance.GetComponent<PlayerInput>();
        _InputControl.BindInputControls(_PlayerInput);
    }

    private void RemoveTemporaryDebugInputHandler()
    {
        if (this.m_DebugInputController == null) return;
        
        IInputControl _InputControl = this.m_DebugInputController.GetComponent<IInputControl>();
        PlayerInput _PlayerInput = InputManager.Instance.GetComponent<PlayerInput>();
        _InputControl.UnbindInputControls(_PlayerInput);
        
        Destroy(this.m_DebugInputController);
    }

    #endregion Methods
  
}
