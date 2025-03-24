using ProjectExodus.Management.UserInterfaceManager;
using UnityEngine;

public class ChatboxHUDController : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private ChatboxHUDView m_View;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_View = this.GetComponent<ChatboxHUDView>();

        IUIEventCollection _EventCollection = UserInterfaceManager.Instance.EventCollectionRegistry;
        _EventCollection.RegisterEvent(ChatboxHUDConstants.CreateChatbox, inputText => this.SetText(inputText as string));
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    private void SetText(string text)
    {
        this.m_View.ShowGUI();
        this.m_View.SetChatText(text);
    }

    #endregion Methods
  
}
