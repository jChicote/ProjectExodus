using ProjectExodus.Management.UserInterfaceManager;

public class Debug_ChatboxHUD : IDebugCommandRegistrater
{

    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _CreateChatbox = new DebugCommand(
            "ui_createchatbox",
            "Create chatbox with default text",
            "ui_createchatbox",
            this.GenerateChatboxText);
            
        debugCommandSystem.RegisterCommand(_CreateChatbox);
    }

    private void GenerateChatboxText()
    {
        string _DefaultText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                              "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud " +
                              "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure " +
                              "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                              "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit " +
                              "anim id est laborum";
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(ChatboxHUDConstants.CreateChatbox, _DefaultText);
    }

    #endregion Methods
  
}
