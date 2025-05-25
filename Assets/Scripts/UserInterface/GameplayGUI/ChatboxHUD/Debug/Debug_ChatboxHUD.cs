using ProjectExodus.Management.UserInterfaceManager;

public class Debug_ChatboxHUD : IDebugCommandRegistrater
{

    #region - - - - - - Fields - - - - - -

    private const string DEFAULT_TEXT = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                        "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud " +
                                        "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure " +
                                        "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                                        "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit " +
                                        "anim id est laborum";

    #endregion Fields
  
    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _CreateChatbox = new DebugCommand(
            "ui_createchatbox",
            "Create chatbox with default text",
            "ui_createchatbox",
            this.GenerateChatboxText);
        DebugCommand _CreateFocusedChatbox = new DebugCommand(
            "ui_createfocusedchatbox",
            "Create focused chatbox with default text",
            "ui_createfocusedchatbox",
            this.GenerateFocusedChatboxText);
            
        debugCommandSystem.RegisterCommand(_CreateChatbox);
        debugCommandSystem.RegisterCommand(_CreateFocusedChatbox);
    }

    private void GenerateChatboxText()
    {
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(ChatboxHUDConstants.CreateChatbox, DEFAULT_TEXT);
    }

    private void GenerateFocusedChatboxText()
    {
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(ChatboxHUDConstants.CreateFocusedChatbox, DEFAULT_TEXT);
    }

    #endregion Methods
  
}
