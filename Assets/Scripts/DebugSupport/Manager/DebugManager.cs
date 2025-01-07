using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebugManager : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public static DebugManager Instance;

    public List<object> CommandList = new();
    
    [SerializeField] private DebugConsole m_Console;

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

    private void Start() 
        => this.InitializeCommands();

    #endregion Unity Methods

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
    }

    #endregion Methods
  
}
